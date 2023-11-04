using System;
using System.Collections.Generic;
using System.IO;

namespace TenderProject.Model
{
    public class TenderInfo
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string TenderStatus { get; set; }
        public ProcedureInfo ProcedureInfo { get; set; }
        public Customer Customer { get; set; }
    }
}
