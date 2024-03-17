using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using TenderProject.Model.BuisnessDomain;

namespace TenderProject
{
    public class JsonDataProvider : DataProviderBase
    {
        public override List<TenderInfo> Get(string path)
        {
            try
            {
                // Прочитайте содержимое файла JSON
                string jsonContent = File.ReadAllText(path);

                // Десериализуйте JSON в список TenderInfo
                List<TenderInfo> tenderList = JsonSerializer.Deserialize<List<TenderInfo>>(jsonContent);

                // Верните список тендеров
                return tenderList;
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при чтении файла или десериализации JSON, выведите сообщение об ошибке
                MessageBox.Show($"Error reading file {path}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TenderInfo>(); // Верните пустой список в случае ошибки
            }
        }

        public override void Set(string path, List<TenderInfo> tenders)
        {
           
            // Реализация записи данных в JSON файл
            string jsonContent = JsonSerializer.Serialize(tenders); // Сериализуем список тендеров в JSON
            File.WriteAllText(path, jsonContent); // Записываем JSON-контент в файл
        }
    }
}
