using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    }

    public class SystemSettings
    {
        public TenderStatus Status { get; set; }
    }

    public class TenderStatus
    {
        public List<string> Items { get; set; }
    }
}
