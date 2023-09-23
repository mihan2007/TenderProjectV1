using System;
using System.IO;
using System.Windows;
using TenderProject.Model;
using System.Text.Json;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
        private TenderInfo _tenderInfo;

        //private string _newFilePath;

        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public PassportTender()
        {
            InitializeComponent();
        }

        public void InitializeTenderInfo(TenderInfo tenderInfo)
        {
            DataContext = tenderInfo;
            _tenderInfo = tenderInfo;

            var tenderStatuses = new System.Collections.ObjectModel.ObservableCollection<string>
            {
                "Статус 1",
                "Статус 2",
                "Статус 3"
            };

            TenderStatus.ItemsSource = tenderStatuses;

            TenderStatus.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (_tenderInfo != null)
            {
                SaveData(_tenderInfo.FilePath, "TenderInfo");
            }
            else
            {
                CreateJsonFile();

            }

            this.Close();
        }

        private int countFilesInFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            return files.Length;
        }

        private void SaveData(string filePath, string operationType)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("File path is empty.");
                    return;
                }

                string[] dataToSave;

                if (operationType == "TenderInfo")
                {
                    dataToSave = _tenderInfo.GetRawData();
                }
                else if (operationType == "TextFields")
                {
                    string[] fieldValues = new string[]
                    {
                SubjectTextBox.Text,
                CustomerTextBox.Text,
                ExpirationDateTextBox.Text,
                LawTextBox.Text,
                LinkTextBox.Text
                    };

                    dataToSave = fieldValues;
                }
                else
                {
                    MessageBox.Show("Invalid operation type.");
                    return;
                }

                File.WriteAllLines(filePath, dataToSave);

                if (operationType == "TextFields")
                {

                    mainWindow.UpdateTenderList();
                }

                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.ToString()}");
            }
        }

        private void CreateJsonFile()
        {
            var jsonObject = new[]
            {
        new
        {
            Subject = SubjectTextBox.Text,
            Customer = CustomerTextBox.Text,
            ExpirationDate = ExpirationDateTextBox.Text,
            Law = LawTextBox.Text,
            FilePath = LinkTextBox.Text,
            TenderStatus = "4242" 
        }
    };

            string jsonData = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });

            string newJsonFilePath = MainWindow.DirectoryPath + (countFilesInFolder(MainWindow.DirectoryPath) + 1) + "." + MainWindow.Extension;

            File.WriteAllText(newJsonFilePath, jsonData);

            MessageBox.Show(newJsonFilePath);

            mainWindow.UpdateTenderList();
        }
    }
}

