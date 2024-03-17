using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        private DataProviderBase _provider;
        public TendersCollection(DataProviderBase dataProvider) // конструктор, который вызывается при создании экземпляра класса (new) 
        {
            _provider = dataProvider;
            Tenders = new List<TenderInfo>(); // создается пустой список тендеров 
        }

        public void Load(string directoryPath) // метод загрузки  данных с диска  в List<TenderInfo> 
        {
            try
            {
                //string jsonContent = File.ReadAllText(directoryPath); // читаем содержимое файла
                //List<TenderInfo> tenders = JsonSerializer.Deserialize<List<TenderInfo>>(jsonContent); // десериализуем JSON в список TenderInfo
                
                List<TenderInfo> tenders = _provider.Get(directoryPath); // десериализуем JSON в список TenderInfo
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
            var newTender = new TenderInfo();  // создаем новый экземпляр класса
            newTender.ProcedureInfo = new ProcedureInfo(); // создаем пустое знаечени поля ProcedureInfo
            newTender.Customer = new Customer(); // создаем пустое знаечени поля Customer 
            Tenders.Add(newTender); // Добовляем в список тендеров новое значение 
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

        public void Remove(TenderInfo tenderToDelete)
        {
            Tenders.Remove(tenderToDelete); // Удаляем указанный тендер из списка
        }

    }
}
