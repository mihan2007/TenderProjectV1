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
            else
            {
                createNewFile();
            }

            this.Close();
        }

        private void createNewFile()
        {
            var newFilePath = MainWindow.DirectoryPath + (countFilesInFolder(MainWindow.DirectoryPath)+1)+ "." +MainWindow.Extension;
            MessageBox.Show(newFilePath);
            File.Create(newFilePath).Close();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.UpdateTenderList();
        }

        private int countFilesInFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            return files.Length;
        }
    }
}

