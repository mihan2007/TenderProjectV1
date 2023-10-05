using System;
using System.IO;
using System.Windows;
using TenderProject.Model;
using System.Text.Json;
using System.Collections.Generic;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
        private TenderInfo _tenderInfo;

        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public PassportTender()
        {
            InitializeComponent();
        }

        public void InitializeTenderInfo(TenderInfo tenderInfo)
        {
            
            DataContext = tenderInfo;
            _tenderInfo = tenderInfo;
        
            if (File.Exists(MainWindow.SytemSettingFilePath))
            {
                try
                {
                    ReadAndAddTenderStatus(tenderInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading system info: {ex}");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (_tenderInfo != null)
            {
                SaveData(_tenderInfo.FilePath);
            }
            else
            {
                string newJsonFilePath = MainWindow.DirectoryPath + (countFilesInFolder(MainWindow.DirectoryPath) + 1) + "." + MainWindow.Extension;
                SaveData(newJsonFilePath);
            }

            this.Close();
        }

        private int countFilesInFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            return files.Length;
        }

        private void SaveData(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("File path is empty.");
                    return;
                }

                string selectedTenderStatus = TenderStatus.SelectedItem as string;

                var jsonObject = new[]
                {
                    new
                    {
                        Subject = SubjectTextBox.Text,
                        Customer = CustomerTextBox.Text,
                        ExpirationDate = ExpirationDateTextBox.Text,
                        Law = LawTextBox.Text,
                        FilePath = filePath,
                        TenderStatus = selectedTenderStatus
                    }
                };

                string jsonData = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

                mainWindow.UpdateTenderList();
                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex}");
            }
        }

        public void ReadAndAddTenderStatus(TenderInfo tenderInfo)
        {
            var tenderStatuses = new System.Collections.ObjectModel.ObservableCollection<string>();
            string jsonContent = File.ReadAllText(MainWindow.SytemSettingFilePath);
            SystemSettings systemInfo = JsonSerializer.Deserialize<SystemSettings>(jsonContent);

            if (tenderInfo == null)
            {
                TenderStatus.ItemsSource = tenderStatuses;
            }
            else
            {
                TenderStatus.SelectedItem = tenderInfo.TenderStatus;
                TenderStatus.ItemsSource = tenderStatuses;
            }

            if (systemInfo != null && systemInfo.Status != null)
            {
                foreach (var status in systemInfo.Status.Items)
                {
                    tenderStatuses.Add(status);
                }
            }
        }

        private void ReadTenderStatus() { 
        }
        private void CreateSystemSettingsFile()
        {
            SystemSettings systemSettings = new SystemSettings();
            systemSettings.Status = new TenderStatus();
            systemSettings.Status.Items = new List<string>
            {
                "Статус 1",
                "Статус 2",
                "Статус 3"
            };

            string jsonData = JsonSerializer.Serialize(systemSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(MainWindow.SytemSettingFilePath, jsonData);
        }
    }
}

