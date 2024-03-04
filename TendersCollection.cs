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
        public List<TenderInfo> Tenders {  get; private set; } // поле класса (property), в котором храняться список тендеров

        public const string Extension = "json"; // расштрение файлов которые необходимо прочитать 

        public TendersCollection() // конструктор, который вызывается при создании экземпляра класса (new) 
        {
            Tenders = new List<TenderInfo>(); // создается пустой список тендеров 
        }
        public void Load(string directoryPath) // метод загрузки  данных с диска  в List<TenderInfo> 
        {
            try
            {
                string jsonContent = File.ReadAllText(directoryPath); // читаем содержимое файла
                List<TenderInfo> tenders = JsonSerializer.Deserialize<List<TenderInfo>>(jsonContent); // десериализуем JSON в список TenderInfo
                Tenders.Clear(); // очищаем Tenders перед новым заполнением
                if (tenders != null)
                {
                    Tenders.AddRange(tenders); // добавляем все элементы из только что десериализованного списка
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deserializing file {directoryPath}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public TenderInfo AddNew()
        {
            
            var newTender = new TenderInfo();
            newTender.Customer = new Customer();
            //newTender.Customer.Name = "new tender";
            Tenders.Add(newTender);
            return newTender;
        }

        public void Save(string direcrotyPath) 
        {
            try
            {
                
                string jsonContent = JsonSerializer.Serialize(Tenders); // Сериализуем список тендеров в JSON

                File.WriteAllText(direcrotyPath, jsonContent); // Записываем JSON-контент в файл
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
