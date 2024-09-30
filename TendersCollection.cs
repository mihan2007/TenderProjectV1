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
        public List<TenderInfo> Tenders { get; private set; } // поле класса (property), в котором храняться список тендеров

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

            _provider.Set(direcrotyPath, Tenders);

        }

        public void Remove(TenderInfo tenderToDelete)
        {
            Tenders.Remove(tenderToDelete); // Удаляем указанный тендер из списка
        }

        public void SortTendersByApplicationDeadlineDateAscending()
        {
            //Tenders.Sort((t1, t2) => CompareDates(t1.ProcedureInfo.ApplicationDeadlineDate, t2.ProcedureInfo.ApplicationDeadlineDate));
        }

        private int CompareDates(string date1, string date2)
        {
            DateTime parsedDate1, parsedDate2;

            // Пытаемся разобрать дату1
            if (!DateTime.TryParseExact(date1, new string[] { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy" }, null, System.Globalization.DateTimeStyles.None, out parsedDate1))
            {
                // Если разбор даты1 не удался, считаем их равными
                return 0;
            }

            // Пытаемся разобрать дату2
            if (!DateTime.TryParseExact(date2, new string[] { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy" }, null, System.Globalization.DateTimeStyles.None, out parsedDate2))
            {
                // Если разбор даты2 не удался, считаем, что дата2 бесконечно поздняя
                return 1;
            }

            // Сравниваем даты сегодняшнего дня без учета года
            DateTime today = DateTime.Today;
            DateTime dateOnly1 = new DateTime(today.Year, parsedDate1.Month, parsedDate1.Day);
            DateTime dateOnly2 = new DateTime(today.Year, parsedDate2.Month, parsedDate2.Day);

            // Если дата1 меньше или равна сегодняшней дате, возвращаем -1
            if (dateOnly1 <= today)
            {
                return -1;
            }

            // Возвращаем результат стандартного сравнения
            return DateTime.Compare(dateOnly1, dateOnly2);
        }
    }
}