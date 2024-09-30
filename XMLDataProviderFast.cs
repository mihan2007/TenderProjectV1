using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using TenderProject.Model.BuisnessDomain;

namespace TenderProject
{
    internal class XMLDataProviderFast : DataProviderBase
    {
        public override List<TenderInfo> Get(string path)
         {
             XmlSerializer serializer = new XmlSerializer(typeof(List<TenderInfo>), new XmlRootAttribute("Tenders"));
             using (FileStream fileStream = new FileStream(path, FileMode.Open))
             {
                 List<TenderInfo> tenders = (List<TenderInfo>)serializer.Deserialize(fileStream);
                 return tenders;
             }
         } 
        /*public override List<TenderInfo> Get(string path)
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
        */
        public override void Set(string path, List<TenderInfo> tenders)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TenderInfo>), new XmlRootAttribute("Tenders"));
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fileStream, tenders);
            }
        }

    }
}
