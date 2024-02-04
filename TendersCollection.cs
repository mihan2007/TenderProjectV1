using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using TenderProject.Model.BuisnessDomain;
using TenderProject.Utilities;

namespace TenderProject
{
    public class TendersCollection
    {
        public List<TenderInfo> Tenders {  get; private set; }

        public const string Extension = "json";

        public TendersCollection()
        {
            Tenders = new List<TenderInfo>();
        }
        public void Load(string directoryPath)
        {
            string[] filePaths = FileHelper.GetFilesInDirectoryWithExtension(directoryPath, Extension);

            Tenders.Clear();

            foreach (var filePath in filePaths)
            {
                try
                {
                    string jsonContent = File.ReadAllText(filePath);
                    List<TenderInfo> tenders = JsonSerializer.Deserialize<List<TenderInfo>>(jsonContent);
                    if (tenders != null)
                    {
                        Tenders.AddRange(tenders);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error deserializing file {filePath}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public void Save() 
        {
        
        }
    }
}
