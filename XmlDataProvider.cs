using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using TenderProject.Model.BuisnessDomain;

namespace TenderProject
{
    public class XmlDataProvider : DataProviderBase
    {
        public override List<TenderInfo> Get(string path)
        {
            try
            {
                // Прочитайте содержимое файла XML
                XDocument xmlDocument = XDocument.Load(path);

                // Создайте список TenderInfo
                List<TenderInfo> tenderList = new List<TenderInfo>();

                // Проход по каждому элементу тендера в XML и добавление его в список
                foreach (XElement tenderElement in xmlDocument.Root.Elements("Tender"))
                {
                    TenderInfo tender = new TenderInfo
                    {
                        ProcedureInfo = new ProcedureInfo
                        {
                            Number = tenderElement.Element(nameof(ProcedureInfo)).Element("Number").Value,
                            Law = tenderElement.Element(nameof(ProcedureInfo)).Element("Law").Value,
                            Type = tenderElement.Element(nameof(ProcedureInfo)).Element("Type").Value,
                            Subject = tenderElement.Element(nameof(ProcedureInfo)).Element("Subject").Value
                        },
                        Customer = new Customer
                        {
                            Name = tenderElement.Element(nameof(Customer)).Element("Name").Value,
                            PostAdress = tenderElement.Element(nameof(Customer)).Element("PostAdress").Value
                        }
                    };
                    tenderList.Add(tender);
                }

                // Верните список тендеров
                return tenderList;
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при чтении файла XML, выведите сообщение об ошибке
                MessageBox.Show($"Error reading file {path}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<TenderInfo>(); // Верните пустой список в случае ошибки
            }
        }

        public override void Set(string path, List<TenderInfo> tenders)
        {
            try
            {
                // Создание XML документа
                XDocument xmlDocument = new XDocument(new XElement("Tenders"));

                // Добавление каждого тендера в XML документ
                foreach (TenderInfo tender in tenders)
                {
                    XElement tenderElement = new XElement("Tender",
                        new XElement("ProcedureInfo",
                            new XElement("Number", tender.ProcedureInfo.Number),
                            new XElement("Law", tender.ProcedureInfo.Law),
                            new XElement("Type", tender.ProcedureInfo.Type),
                            new XElement("Subject", tender.ProcedureInfo.Subject)
                        ),
                        new XElement("Customer",
                            new XElement("Name", tender.Customer.Name),
                            new XElement("PostAdress", tender.Customer.PostAdress)
                        )
                    );
                    xmlDocument.Root.Add(tenderElement);
                }

                // Сохранение XML документа в файл
                xmlDocument.Save(path);
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при записи в файл XML, выведите сообщение об ошибке
                MessageBox.Show($"Error writing to file {path}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
