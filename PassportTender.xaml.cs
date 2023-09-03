using System.Windows;
using System.IO;
using System;
using TenderProject.Model;

namespace TenderProject
{
    public partial class PassportTender : Window
    {
        private TenderInfo _tenderInfo;

        public string newFilePath;
        public string FilePath { get; set; }

        public PassportTender()
        {
            InitializeComponent();
        }

        public void InitializeTenderInfo(TenderInfo tenderInfo)
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
                SaveToFile(_tenderInfo.FilePath); 
            }
            else
            {
                createNewFile();
                SaveToNewFile(newFilePath);
            }



            this.Close();
        }

        private void SaveToNewFile(string newFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(newFilePath))
                {
                    MessageBox.Show("File path is empty.");
                    return;
                }

                // Соберем значения всех текстовых полей в массив строк
                string[] fieldValues = new string[]
                {
                    SubjectTextBox.Text,
                    CustomerTextBox.Text,
                    ExpirationDateTextBox.Text,
                    LawTextBox.Text,
                    LinkTextBox.Text
                };

                // Запишем массив строк в файл
                File.WriteAllLines(newFilePath, fieldValues);
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.UpdateTenderList();
                MessageBox.Show("Values saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving values: {ex.ToString()}");
            }
        }

        private void createNewFile()
        {
            newFilePath = MainWindow.DirectoryPath + (countFilesInFolder(MainWindow.DirectoryPath)+1)+ "." +MainWindow.Extension;
            MessageBox.Show(newFilePath);
            File.Create(newFilePath).Close();
        }

        private int countFilesInFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            return files.Length;
        }
    }
}

