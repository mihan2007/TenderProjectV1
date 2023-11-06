using System;
using System.IO;
using System.Windows;
using TenderProject.Model;
using System.Text.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
namespace TenderProject
{
    public partial class PassportTender : Window
    {
 
        private TenderInfo _tenderInfo;
        private bool _EditionMode;

        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        public PassportTender()
        {
            InitializeComponent();
            Closing += PassportTender_Closing;
        }

        public void InitializeTenderInfo(TenderInfo tenderInfo)
        {
            
            DataContext = tenderInfo;
            _tenderInfo = tenderInfo;

            SetReadOnlyForAllTextFields(true);

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
            if (!_EditionMode)
            {
                _EditionMode = true;
                EditBeutton.Content = "Сохранить";
                SetReadOnlyForAllTextFields(false);
            }
            else
            {
                _EditionMode = false;
                EditBeutton.Content = "Редактировать";

                if (_tenderInfo != null)
                {
                    SaveData(_tenderInfo.FilePath,true);
                }
                else
                {
                    CreateNewTenderFile();
                }
                SetReadOnlyForAllTextFields(true);
            }
        }

        private int countFilesInFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            return files.Length;
        }

        private void SaveData(string filePath, bool ShowSaveWinodw)
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
                        FilePath = filePath,
                        TenderStatus = selectedTenderStatus,

                        ProcedureInfo = new
                        {
                            Number =  ProcuderNumberTextBox.Text,
                            Law =  ProcedureLawTextBox.Text,
                            Type =  ProcedureTypeTextBox.Text,
                            Subject =  ProcedureTypeSubjectTextBox.Text,
                            Stage =  ProcedureStageTextBox.Text,
                            SmallBusinessProcedure =  SmallBusinessProcedureTextBox.Text,
                            TradePlatformName =  ProcedureTradePlatformNameTextBox.Text,
                            TradePlatformSite =  "N/A",
                            ProcedureLink =  ProcedureLinkTextBox.Text,
                            PublicationDate =  ProcedurePublicationDateTextBox.Text,
                            ApplicationDeadlineDate =  ProcedureApplicationDeadlineDateTextBox.Text,
                            AuctionDate =  ProcedureAuctionDateTextBox.Text,
                            SummarizingDate =  ProcedureSummarizingDateTextBox.Text,
                            InitialPrice =  ProcedureInitialPriceTextBox.Text,
                            ApplicationSecurityDeposit = ProcedureApplicationSecurityDepositTextBox.Text,
                            ContractSecurityDeposit =  ProcedureContractSecurityDepositTextBox.Text

                        },
                        Customer = new
                        {
                            Name =  CustomerNameTextBox.Text,
                            PostAdress =  "n/a",
                            INN =  CustomerInnTexBox.Text,
                            KPP =  CustomerKppTexBox.Text,
                            OGRN =  "null",
                            Adress =  "null",
                            ResponsiblePerson =  "null",
                            Email =  "null",
                            PhoneNumber =  "null",
                            Comments =  "null"

                        },
                    }
                };

                string jsonData = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);

                mainWindow.UpdateTenderList();

                if (ShowSaveWinodw)
                {
                    MessageBox.Show("Data saved successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex}");
            }
        }


        public void ReadAndAddTenderStatus(TenderInfo tenderInfo)
        {
            var tenderStatuses = new System.Collections.ObjectModel.ObservableCollection<string>();
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
                MessageBox.Show($"Error reading system settings: {ex.Message}");
            }

            TenderStatus.ItemsSource = tenderStatuses;

            if (tenderInfo != null)
            {
                TenderStatus.SelectedItem = tenderInfo.TenderStatus;
            }
        }


        private void PassportTender_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_EditionMode && _tenderInfo != null)
            {
                SaveData(_tenderInfo.FilePath, false);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
               
                if (result == MessageBoxResult.Yes)
                {
                    CreateNewTenderFile();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;  
                }
            }
        }
 
        private void SetReadOnlyForAllTextFields(bool isReadOnly)
        {
            foreach (UIElement child in MainProcedureInfoBox.Children)
            {
                if (child is TextBox textBox)
                {
                    textBox.IsReadOnly = isReadOnly;
                }
            }

            foreach (UIElement child in CustomerInfoBox.Children)
            {
                if (child is TextBox textBox)
                {
                    textBox.IsReadOnly = isReadOnly;
                }
            }
        }

        private void CreateNewTenderFile()
        {
            string newJsonFilePath = MainWindow.DirectoryPath + (countFilesInFolder(MainWindow.DirectoryPath) + 1) + "." + MainWindow.Extension;
            SaveData(newJsonFilePath, true);
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

