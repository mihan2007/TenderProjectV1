using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace TenderProject.Model
{
    public class TenderInfo
    {
        public string Subject { get; set; }
        public string Customer { get; set; }
        public string ExpirationDate { get; set; }
        public string Law { get; set; }
        public string Link { get; set; }
        public string FilePath { get; set; }
        public string TenderStatus { get; set; }
        public ProcedureInfo ProcedureType { get; set; }

    }
    public class ProcedureInfo
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string SmallBusinessProcedure { get; set; }
        public string TradePlatformName { get; set; }
        public string TradePlatformSite { get; set; }
        public string ProcedureLink { get; set; }
        public string PublicationDate { get; set; }
        public string ApplicationDeadlineDate { get; set; }
        public string AuctionDate { get; set; }
        public string SummarizingDate { get; set; }
        public string InitialPrice { get; set; }
        public string ApplicationSecurityDeposit { get; set; }
        public string ContractSecurityDeposit { get; set; }

    }
    public class Customer
    {
        public string Name { get; set; }
        public string PostAdress { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string OGRN { get; set; }
        public string Adress { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }

    }

    public class DataSaver
    {
        public void CreateEmptyJsonFile(string filePath)
        {
            TenderInfo emptyTenderInfo = new TenderInfo
            {
                Subject = null,
                Customer = null,
                ExpirationDate = null,
                Law = null,
                Link = null,
                FilePath = null,
                TenderStatus = null,
                ProcedureType = new ProcedureInfo
                {
                    Number = null,
                    Type = null,
                    Subject = null,
                    SmallBusinessProcedure = null,
                    TradePlatformName = null,
                    TradePlatformSite = null,
                    ProcedureLink = null,
                    PublicationDate = null,
                    ApplicationDeadlineDate = null,
                    AuctionDate = null,
                    SummarizingDate = null,
                    InitialPrice = null,
                    ApplicationSecurityDeposit = null,
                    ContractSecurityDeposit = null
                },
            };

            string json = JsonConvert.SerializeObject(emptyTenderInfo, Formatting.Indented);

            // Сохраняем JSON в файл
            File.WriteAllText(filePath, json);
        }
    }

}
