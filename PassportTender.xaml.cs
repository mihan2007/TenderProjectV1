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
                    dynamic systemInfo = JsonSerializer.Deserialize<dynamic>(jsonContent);


                    if (systemInfo != null && systemInfo.Status != null)
                    {
                        foreach (var status in systemInfo.Status)
                        {
                            tenderStatuses.Add(systemInfo.Status.ToString());
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading system info: {ex}");
                }
            }

            TenderStatus.ItemsSource = tenderStatuses;

            if (!string.IsNullOrEmpty(tenderInfo.TenderStatus) && tenderStatuses.Contains(tenderInfo.TenderStatus))
            {
                TenderStatus.SelectedItem = tenderInfo.TenderStatus;
            }
            else
            {
                TenderStatus.SelectedIndex = 0;
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
    }
}

