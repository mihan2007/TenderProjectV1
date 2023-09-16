using System;
using System.Collections.Generic;
using System.Text;

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

        //public Dictionary<string, string> ExtendedFields;

        public string TenderStatus { get; set; } 

        public TenderInfo(string[] rawData, string filePath)
        {
            for (int i = 0; i < rawData.Length; i++)
            {
                if (string.IsNullOrEmpty(rawData[i]))
                {
                    rawData[i] = "NA";
                }
            }
            if (rawData.Length >= 5) 
            {
                Subject = rawData[0];
                Customer = rawData[1];
                ExpirationDate = rawData[2];
                Law = rawData[3];
                Link = rawData[4];
                //TenderStatus = rawData[5];
                FilePath = filePath;
            }
        }

        public string[] GetRawData()
        {
            string[] result = new string[]
            {
                Subject,
                Customer,
                ExpirationDate,
                Law,
                Link,
                //TenderStatus 
            };

            return result;
        }
    }
}
