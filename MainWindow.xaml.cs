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

namespace TenderProject
{

    public partial class MainWindow : Window
    {
        public const string DirectoryPath = @"C:\tenderproject\";
        public const string Extension = "txt";

        private List<TenderInfo> tenderItems = new List<TenderInfo>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            SearchButton.Click += SeatchButtonClick;

        }



        private  void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(DirectoryPath, Extension);

            foreach (string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath);
                var tender = new TenderInfo(lines, filePath);
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
                string[] lines = File.ReadAllLines(filePath);
                TenderInfo tender = new TenderInfo(lines, filePath);
                tenderItems.Add(tender);
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

        private void SeatchButtonClick(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                // If the search term is empty or null, display all tenders
                TenderList.ItemsSource = tenderItems;
            }
            else
            {
                // Filter tenders based on the search term (case-insensitive)
                var filteredTenders = tenderItems
                    .Where(tender => tender.Customer.ToLower().Contains(searchTerm))
                    .ToList();

                TenderList.ItemsSource = filteredTenders;
            }

        }

    }
}
