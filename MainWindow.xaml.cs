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
using System.Xml;
using System.Windows.Media;
using HighlightTextBlockControl;


namespace TenderProject
{

    public partial class MainWindow : Window
    {

        public const string DirectoryPathJsonFile = @"C:\tenderproject\1.json";

        public const string DirectoryPathXMLFile = @"C:\tenderproject\File.xml";

        public const string SytemSettingFilePath = "SystemSettings\\SystemSetting.json";

        private TendersCollection _tendersCollection;

        //public event Action SelfLoaded; // пример кастомного события
        public MainWindow()
        {
            InitializeComponent(); // инициализируем собственные визуальные компоненты, вызоы который требует фремфорк
            
            Loaded += MainWindow_Loaded; // пдписка на событие Loaded (стандртное событие любого окна) когда окно будет загруженно, будет вызван метод MainWindow_Loaded

            SearchButton.Click += SearchButtonClick; // подписка на событие при нажатии кнопуи поиска 

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            _tendersCollection = new TendersCollection(new XmlDataProvider());
            _tendersCollection.Load(DirectoryPathXMLFile);
            UpdateTendersInternal(); // обновляем список тендеров
        }


        private void UpdateTendersInternal()
        {
            TenderList.ItemsSource = null; // вызывает обновление окна, хак 

            TenderList.ItemsSource = _tendersCollection.Tenders; // визцальному компоненту TenderList, отображаещему список тендеров передаем только что загруженные тенедры 
            
        }

        private void AddNewTenderButtonClick(object sender, RoutedEventArgs e)
        {
           
            var newTender = _tendersCollection.AddNew();
            OpenPassportTenderWindow(newTender);  
            UpdateTendersInternal();  // нужно удалить когда заработает PasportTender

        }

        private void OpenPassportTenderWindow(TenderInfo source)
        {
            PassportTender passportTender = new PassportTender(); // создвем новый экземпляр класса pasporttender
            passportTender.Initialize(source);          // инициализируем паспорт тендера, заполняем значения
            passportTender.TenderChanged += PassportTender_TenderChanged; // подписываемся на событие было ли изменение
            passportTender.Show();  // отображаем паспорт тендера
        }

        private void PassportTender_TenderChanged(TenderInfo obj)
        {
            UpdateTendersInternal();

            _tendersCollection.Save(DirectoryPathXMLFile);
        }

        private void TenderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTender = (TenderInfo)TenderList.SelectedItem;

            OpenPassportTenderWindow(selectedTender);
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
                TenderList.ItemsSource = _tendersCollection.Tenders;
            }
            else
            {
                foreach (var tender in _tendersCollection.Tenders)
                {
                    var listBoxItem = TenderList.ItemContainerGenerator.ContainerFromItem(tender) as ListBoxItem;
                    if (listBoxItem != null)
                    {
                        HighlightTextBlock highlightTextBlock = FindVisualChild<HighlightTextBlock>(listBoxItem);
                        if (highlightTextBlock != null)
                        {
                           

                            highlightTextBlock.HighlightText = searchTerm;
                        }
                    }
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Вызываем метод SearchButtonClick для выполнения поиска
            //SearchButtonClick(sender, e);
        }


        private void DeletTenderClick(object sender, RoutedEventArgs e)
        {
            var selectedTender = (TenderInfo)TenderList.SelectedItem;
            _tendersCollection.Remove(selectedTender);
            UpdateTendersInternal();
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
                    foreach (var tender in _tendersCollection.Tenders)
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
            File.WriteAllText("C:\\tenderproject\\1", json);
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

        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
