using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TenderProject.Model
{
    public class TenderInfo
    {
        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }

        [JsonPropertyName("expirationDate")]
        public string ExpirationDate { get; set; }

        [JsonPropertyName("law")]
        public string Law { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("filePath")]
        public string FilePath { get; set; }

        [JsonPropertyName("tenderStatus")]
        public string TenderStatus { get; set; }

        public TenderInfo() { }  // Parameterless constructor needed for deserialization

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
                Link
            };

            return result;
        }
    }
}
