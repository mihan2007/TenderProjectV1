using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using TenderProject.Model;
using TenderProject.Infrastructure;
using System.Linq;
using System.Text.Json;


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

        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);

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
                        tender.Customer.ToLower().Contains(searchTerm) ||
                        tender.Subject.ToLower().Contains(searchTerm) ||
                        tender.ExpirationDate.ToLower().Contains(searchTerm) ||
                        tender.Law.ToLower().Contains(searchTerm))
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
            ExportToExcel(tenderItems);
        }

        private void ExportToExcel(List<TenderInfo> tenders) { }
    }
}
