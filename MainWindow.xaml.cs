using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace TenderProject
{

    public partial class MainWindow : Window
    {
        public const string DirectoryPath = @"C:\tenderproject\";
        public const string Extension = "txt";

        public class TenderInfo
        {
            public string Subject { get; set; }
            public string Customer { get; set; }
            public string ExpirationDate { get; set; }
            public string Law { get; set; }
            public string Link { get; set; }

            public string FilePath { get; set; }



            public TenderInfo(string[] rawData, string filePath)
            {
                for (int i = 0; i < rawData.Length; i++)
                {
                    if (string.IsNullOrEmpty(rawData[i]))
                    {
                        rawData[i] = "NA";
                    }
                }
                if (rawData.Length >= 5)
                {
                    Subject = rawData[0];
                    Customer = rawData[1];
                    ExpirationDate = rawData[2];
                    Law = rawData[3];
                    Link = rawData[4];
                    FilePath = filePath;
                }
            }
            public string[] GetRawData() 
            {
                string[] result = new string[]
                {
                    Subject,
                    Customer,
                    ExpirationDate,
                    Law,
                    Link
                };

                return result;
            }
        }

        private List<TenderInfo> tenderItems = new List<TenderInfo>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        public class FileHelper
        {
            public static string[] GetFilesInDirectoryWithExtension(string directoryPath, string extension)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Директория {directoryPath} не существует.");
                    return new string[0];
                }

                try
                {
                    string[] files = Directory.GetFiles(directoryPath, $"*.{extension}");
                    return files;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при поиске файлов: {ex.Message}");
                    return new string[0];
                }
            }
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);

            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                TenderInfo tender = new TenderInfo(lines, filePath);
                tenderItems.Add(tender);
            }

            TenderList.ItemsSource = tenderItems;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PassportTender passportTender = new PassportTender();
            passportTender.Show();
        }

        private void TenderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TenderInfo selectedTender = (TenderInfo)TenderList.SelectedItem;

            PassportTender passportTender = new PassportTender();
            passportTender.InitializeTenderInfo(selectedTender);
            passportTender.Show();
        }

        public void UpdateTenderList()
        {
            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);
            TenderList.ItemsSource = null;
            tenderItems.Clear(); 

            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                TenderInfo tender = new TenderInfo(lines, filePath);
                tenderItems.Add(tender);
            }

            // Устанавливаем источник данных TenderList заново
            TenderList.ItemsSource = tenderItems;
        }
    }
}
