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

            var tenderStatuses = new System.Collections.ObjectModel.ObservableCollection<string>();

            if (File.Exists(MainWindow.SytemSettingFilePath))
            {
                try
                {
                   string jsonContent = File.ReadAllText(MainWindow.SytemSettingFilePath);
                   
                    SystemSettings systemInfo = JsonSerializer.Deserialize<SystemSettings>(jsonContent);

                    if (systemInfo != null && systemInfo.Status != null)
                    {
                        foreach (var status in systemInfo.Status.Items)
                        {
                            tenderStatuses.Add(status);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading system info: {ex}");
                }
            }

            TenderStatus.ItemsSource = tenderStatuses;

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

