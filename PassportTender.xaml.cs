using System.Windows;
using System.IO;
using System;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
        private MainWindow.TenderInfo _tenderInfo;
        public string FilePath { get; set; }

        public PassportTender()
        {
            InitializeComponent();
        }

        public void InitializeTenderInfo(MainWindow.TenderInfo tenderInfo)
        {
            DataContext = tenderInfo;
            _tenderInfo = tenderInfo;
           
        }
        public void SaveToFile(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("File path is empty.");                   
                    return;
                }

                // Convert the updated data to a string array
                string[] updatedData = _tenderInfo.GetRawData();
                if (_tenderInfo == null)
                    return;
                {
                    MessageBox.Show("Update data == null");
                }
                // Write the updated data to the file
                File.WriteAllLines(filePath, updatedData);

                MessageBox.Show("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.ToString()}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (_tenderInfo != null)
            {
                SaveToFile(_tenderInfo.FilePath); // Здесь указываете путь к файлу, куда нужно сохранить информацию
            }

            this.Close();
        }
    }
}

