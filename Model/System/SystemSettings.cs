using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace TenderProject.Model.System
{
    public class SystemSettings
    {
        private static SystemSettings _instance;
        public static SystemSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = IntializeFromFile();

                }
                return _instance;
            }
        }
        public TenderStatus Status { get; set; }
        private static SystemSettings IntializeFromFile()
        {
            if (!File.Exists(MainWindow.SytemSettingFilePath))
            {
                MessageBox.Show($"Error loading system info: System Settings File not exist");
                return null;
            }

            try
            {
                string jsonContent = File.ReadAllText(MainWindow.SytemSettingFilePath);
                return JsonSerializer.Deserialize<SystemSettings>(jsonContent);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading system settings: {ex.Message}");
            }
            return null;
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