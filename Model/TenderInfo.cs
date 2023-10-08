using System;
using System.Collections.Generic;

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

        public List<TenderExtraField> ExtraFieldsList { get; set; } = new List<TenderExtraField>();
        
        [NonSerialized]
        private Dictionary<string, TenderExtraField> _extraFields;
        
        public Dictionary<string, TenderExtraField> ExtraFields
        {
            get
            {
                if (_extraFields == null)
                {
                    _extraFields = CreateDictionary();
                }
                
                return _extraFields;
            }
        }
        
        private Dictionary<string, TenderExtraField> CreateDictionary()
        {
            var dict = new Dictionary<string, TenderExtraField>();
            foreach (var item in ExtraFieldsList)
            {
                if (dict.ContainsKey(item.Id))
                {
                    Console.WriteLine($"Tender {Subject} has duplicate extra field {item.Id}. Value: {item.Value}. Comment: {item.Comment}");
                    continue;
                }

                dict.Add(item.Id, item);
            }

            return dict;
        }
    }
}
