using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Text.Json;
using ClosedXML.Excel;
using Microsoft.Win32;
using TenderProject.Model.BuisnessDomain;
using TenderProject.Model.System;
using TenderProject.Utilities;


namespace TenderProject
{

    public partial class MainWindow : Window
    {
        public const string DirectoryPath = @"C:\tenderproject\";
        public const string Extension = "json";
        public const string SytemSettingFilePath = "SystemSettings\\SystemSetting.json";

        private List<TenderInfo> tenderItems = new List<TenderInfo>();

        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += MainWindow_Loaded;

            SearchButton.Click += SearchButtonClick;

           // ReadAndSetStatusInTextBox(MainWindow.SytemSettingFilePath);

            //GenerateJsonData(@"C:\tenderproject\123456.json");
        }

        public  void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);
            var statuses = ReadAndSetStatusInTextBox(MainWindow.SytemSettingFilePath);

            foreach (string filePath in filePaths)
            {
                try
                {
                    string jsonContent = File.ReadAllText(filePath);
                    List<TenderInfo> tenders = JsonSerializer.Deserialize<List<TenderInfo>>(jsonContent);
                    if (tenders != null)
                    {
                        
                        tenderItems.AddRange(tenders);
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deserializing file {filePath}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            TenderList.ItemsSource = tenderItems;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PassportTender passportTender = new PassportTender();
            passportTender.InitializeTenderInfo(null);
            passportTender.SetReadOnlyForAllTextFields(false);
            passportTender._EditionMode = true;
            passportTender.Show();
        }

        private void TenderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTender = (TenderInfo)TenderList.SelectedItem;
            var passportTender = new PassportTender();
            passportTender.InitializeTenderInfo(selectedTender);

            passportTender.Show();
        }

        public void UpdateTenderList()
        {
            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);
            TenderList.ItemsSource = null;
            tenderItems.Clear();

            foreach (var filePath in filePaths)
            {
                try
                {
                    string jsonContent = File.ReadAllText(filePath);
                    List<TenderInfo> tenders = JsonSerializer.Deserialize<List<TenderInfo>>(jsonContent);
                    if (tenders != null)
                    {
                        tenderItems.AddRange(tenders);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error deserializing file {filePath}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            TenderList.ItemsSource = tenderItems;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();                
            }
        }

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                TenderList.ItemsSource = tenderItems;
            }
            else
            {
                var filteredTenders = tenderItems
                    .Where(tender =>
                        tender.Customer.Name.ToLower().Contains(searchTerm) ||
                        tender.ProcedureInfo.Number.ToLower().Contains(searchTerm) ||
                        tender.ProcedureInfo.Subject.ToLower().Contains(searchTerm))
                    .ToList();

                TenderList.ItemsSource = filteredTenders;
            }
        }

        private void DeletTenderClick(object sender, RoutedEventArgs e)
        {
            var selectedTender = (TenderInfo)TenderList.SelectedItem;

            if (selectedTender != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm deleting", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    File.Delete(selectedTender.FilePath);
                    UpdateTenderList();
                }
            }
            else
            {
                MessageBox.Show("Select Tender");
            }
           
        }
        private void ExportToExcelButtonClick(object sender, RoutedEventArgs e)
        {
            //ExcelExporter.ExportToExcel(this, TenderInfo);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx|All Files|*.*",
                Title = "Выберите место сохранения файла",
                FileName = "exported_data.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                if (IsFileInUse(filePath))
                {
                    MessageBox.Show("Файл занят другим процессом. Выберите другое место и/или имя файла.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание нового рабочего книги Excel
                using (var workbook = new XLWorkbook())
                {
                    // Добавление листа в книгу
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    // Получение данных из TenderList и запись в Excel
                    int row = 1;
                    foreach (var tender in tenderItems)
                    {
                        worksheet.Cell($"A{row}").Value = tender.Customer.Name;
                        //worksheet.Cell($"B{row}").Value = tender.ProcedureInfo.Number;
                        //worksheet.Cell($"C{row}").Value = tender.ProcedureInfo.Subject;
                        // ... добавьте другие поля, если необходимо
                        row++;
                    }

                    // Сохранение книги в выбранное место
                    workbook.SaveAs(filePath);
                }

                MessageBox.Show($"Данные экспортированы в Excel-файл: {filePath}", "Успех");
            }
        }

        public static void CreateEmptyJsonFile(string filePath)
        {
            ProcedureInfo procedureInfo = new ProcedureInfo();
            Customer customer = new Customer();

            // Создаем объект, который содержит оба класса
            var data = new { ProcedureInfo = procedureInfo, Customer = customer };

            // Опции для форматирования JSON
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true // Это делает JSON читаемым
            };

            // Сериализуем объект в JSON
            string json = JsonSerializer.Serialize(data, jsonOptions);

            // Указываем путь и имя файла, куда сохранить JSON
            File.WriteAllText(filePath, json);
        }

        static void GenerateJsonData(string filePath)
        {
            // Создаем экземпляры классов с пустыми полями
            TenderInfo tenderInfo = new TenderInfo();
            Customer customer = new Customer();
            ProcedureInfo procedureInfo = new ProcedureInfo();

            tenderInfo.ProcedureInfo = procedureInfo;
            tenderInfo.Customer = customer;
            tenderInfo.FilePath = filePath;

            // Настройки сериализации JSON
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // Сериализуем объект в JSON
            var list = new List<TenderInfo>() { tenderInfo };
            string json = JsonSerializer.Serialize(list, jsonOptions);
            
            // Записываем JSON в файл
            File.WriteAllText(filePath, json);

            Console.WriteLine($"JSON файл создан по пути: {filePath}");
        }
        private List<string> ReadAndSetStatusInTextBox(string filePath)
        {
            List<string> tenderStatuses = new List<string>();
            
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                SystemSettings systemInfo = JsonSerializer.Deserialize<SystemSettings>(jsonContent);

                if (systemInfo != null && systemInfo.Status != null && systemInfo.Status.Items.Count > 0)
                {
                    tenderStatuses = systemInfo.Status.Items;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading data from file: {ex.Message}");
            }

            return tenderStatuses;
        }

        private bool IsFileInUse(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // Если файл открывается без ошибок, это значит, что он не занят другим процессом
                    return false;
                }
            }
            catch (IOException)
            {
                // Если происходит ошибка, это означает, что файл занят другим процессом
                return true;
            }
        }


    }
}
