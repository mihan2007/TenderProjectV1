using System;
using System.IO;
using System.Windows;
using System.Text.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using TenderProject.Model.BuisnessDomain;
using TenderProject.Model.System;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
 
        private TenderInfo _tenderInfo;
        public bool _EditionMode;
        
        

        public event Action<TenderInfo> TenderChanged;

        public PassportTender()
        {
            InitializeComponent();
            Closing += PassportTender_Closing;
        }

        public void Initialize(TenderInfo tenderInfo, bool isReadOnly) // инициализируем паспорт тендера
        {
            
            DataContext = tenderInfo;
            _tenderInfo = tenderInfo;

            SetReadOnlyForAllTextFields(isReadOnly);
            //TenderStatusFill(tenderInfo.TenderStatus);

        }

        private void EditModeButtonClick(object sender, RoutedEventArgs e)
        {
            if (!_EditionMode)
            {
                EditBeutton.Content = "Сохранить";
                SetReadOnlyForAllTextFields(false);
                _EditionMode = true;
            }
            else
            {
                _EditionMode = false;
                EditBeutton.Content = "Редактировать";

                if (_tenderInfo != null)
                {

                   // tendersCollection.Save(MainWindow.DirectoryPath);
                }
                else
                {
                    CreateNewTenderFile();
                }

                SetReadOnlyForAllTextFields(true);
            }
        }

    
        private void SaveData(string filePath, bool ShowSaveWinodw)
        {

            TenderChanged.Invoke(_tenderInfo);
        }


        public void TenderStatusFill(string currentStatus)
        {
            var tenderStatuses = new System.Collections.ObjectModel.ObservableCollection<string>();

            foreach (var status in SystemSettings.Instance.Status.Items)
            {
                tenderStatuses.Add(status);
            }

            TenderStatus.ItemsSource = SystemSettings.Instance.Status.Items;

          
            if (currentStatus != null)
            {
                TenderStatus.SelectedItem = currentStatus;
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
 
        public void SetReadOnlyForAllTextFields(bool isReadOnly)
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
            //_uniqNumber = GenerateUniqueFilename();
            //string newJsonFilePath = MainWindow.DirectoryPath + (GenerateUniqueFilename() + "." + TendersCollection.Extension);
            //SaveData(newJsonFilePath, true);

           
        }

        static long GenerateUniqueFilename()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return long.Parse(timestamp);
        }

    }
}

