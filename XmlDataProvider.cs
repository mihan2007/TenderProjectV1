using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
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
                            Subject = tenderElement.Element(nameof(ProcedureInfo)).Element("Subject").Value,
                            Stage = tenderElement.Element(nameof(ProcedureInfo)).Element("Stage").Value,
                            SmallBusinessProcedure = tenderElement.Element(nameof(ProcedureInfo)).Element("SmallBusinessProcedure").Value,
                            TradePlatformName = tenderElement.Element(nameof(ProcedureInfo)).Element("TradePlatformName").Value,
                            TradePlatformSite = tenderElement.Element(nameof(ProcedureInfo)).Element("TradePlatformSite").Value,
                            ProcedureLink = tenderElement.Element(nameof(ProcedureInfo)).Element("ProcedureLink").Value,
                            PublicationDate = tenderElement.Element(nameof(ProcedureInfo)).Element("PublicationDate").Value,
                            ApplicationDeadlineDate = tenderElement.Element(nameof(ProcedureInfo)).Element("ApplicationDeadlineDate").Value,
                            ApplicationDate = tenderElement.Element(nameof(ProcedureInfo)).Element("ApplicationDate").Value,
                            AuctionDate = tenderElement.Element(nameof(ProcedureInfo)).Element("AuctionDate").Value,
                            SummarizingDate = tenderElement.Element(nameof(ProcedureInfo)).Element("SummarizingDate").Value,
                            InitialPrice = tenderElement.Element(nameof(ProcedureInfo)).Element("InitialPrice").Value,
                            AplicationSecurityDeposit = tenderElement.Element(nameof(ProcedureInfo)).Element("AplicationSecurityDeposit").Value,
                            ContractSecurityDeposit = tenderElement.Element(nameof(ProcedureInfo)).Element("ContractSecurityDeposit").Value,
                            QuataionPrice = tenderElement.Element(nameof(ProcedureInfo)).Element("QuataionPrice").Value,
                            TradePlatformTarif = tenderElement.Element(nameof(ProcedureInfo)).Element("TradePlatformTarif").Value,
                            DeliveryAddress = tenderElement.Element(nameof(ProcedureInfo)).Element("DeliveryAddress").Value,
                            DeliveryTime = tenderElement.Element(nameof(ProcedureInfo)).Element("DeliveryTime").Value,
                            Comments = tenderElement.Element(nameof(ProcedureInfo)).Element("Comments").Value
                        },
                        Customer = new Customer
                        {
                            Name = tenderElement.Element(nameof(Customer)).Element("Name").Value,
                            PostAdress = tenderElement.Element(nameof(Customer)).Element("PostAdress").Value,
                            INN = tenderElement.Element(nameof(Customer)).Element("INN").Value,
                            KPP = tenderElement.Element(nameof(Customer)).Element("KPP").Value,
                            OGRN = tenderElement.Element(nameof(Customer)).Element("OGRN").Value,
                            CustomerContactInfo = tenderElement.Element(nameof(Customer)).Element("CustomerContactInfo").Value

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
                            new XElement("Subject", tender.ProcedureInfo.Subject),
                            new XElement("Stage", tender.ProcedureInfo.Stage),
                            new XElement("SmallBusinessProcedure", tender.ProcedureInfo.SmallBusinessProcedure),
                            new XElement("TradePlatformName", tender.ProcedureInfo.TradePlatformName),
                            new XElement("TradePlatformSite", tender.ProcedureInfo.TradePlatformSite),
                            new XElement("ProcedureLink", tender.ProcedureInfo.ProcedureLink),
                            new XElement("PublicationDate", tender.ProcedureInfo.PublicationDate),
                            new XElement("ApplicationDeadlineDate", tender.ProcedureInfo.ApplicationDeadlineDate),
                            new XElement("ApplicationDate", tender.ProcedureInfo.ApplicationDate),
                            new XElement("AuctionDate", tender.ProcedureInfo.AuctionDate),
                            new XElement("SummarizingDate", tender.ProcedureInfo.SummarizingDate),
                            new XElement("InitialPrice", tender.ProcedureInfo.InitialPrice),
                            new XElement("AplicationSecurityDeposit", tender.ProcedureInfo.AplicationSecurityDeposit),
                            new XElement("ContractSecurityDeposit", tender.ProcedureInfo.ContractSecurityDeposit),
                            new XElement("QuataionPrice", tender.ProcedureInfo.QuataionPrice),
                            new XElement("TradePlatformTarif", tender.ProcedureInfo.TradePlatformTarif),
                            new XElement("DeliveryAddress", tender.ProcedureInfo.DeliveryAddress),
                            new XElement("DeliveryTime", tender.ProcedureInfo.DeliveryTime),
                            new XElement("Comments", tender.ProcedureInfo.Comments)
                        ),
                        new XElement("Customer",
                            new XElement("Name", tender.Customer.Name),
                            new XElement("PostAdress", tender.Customer.PostAdress),
                            new XElement("INN", tender.Customer.INN),
                            new XElement("KPP", tender.Customer.KPP),
                            new XElement("OGRN", tender.Customer.OGRN),
                            new XElement("CustomerContactInfo", tender.Customer.CustomerContactInfo)
                        )
                    );
                    xmlDocument.Root.Add(tenderElement);
                }

                // Сохранение XML документа в файл
                xmlDocument.Save(path, SaveOptions.DisableFormatting); // Отключение форматирования

            }
            catch (Exception ex)
            {
                // Если возникла ошибка при записи в файл XML, выведите сообщение об ошибке
                MessageBox.Show($"Error writing to file {path}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}

