using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            public TenderInfo(string[] rawData)
            {
                Subject = rawData[0];
                Customer = rawData[1];
                ExpirationDate = rawData[2];
                Law = rawData[3];
                Link = rawData[4];
            }
        }

        private List<TenderInfo> tenderItems = new List<TenderInfo>(); // Создаем список для хранения объектов TenderInfo

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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);

            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                TenderInfo tender = new TenderInfo(lines);
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
    }
}
