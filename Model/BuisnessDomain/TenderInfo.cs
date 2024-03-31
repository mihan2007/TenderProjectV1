
namespace TenderProject.Model.BuisnessDomain
{
    public class TenderInfo
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string TenderStatus { get; set; }
        public ProcedureInfo ProcedureInfo { get; set; }
        public Customer Customer { get; set; }

        public TenderInfo Copy() 
        {
            var copy = new TenderInfo();
            copy.Id = Id;
            copy.FilePath = FilePath;              
            copy.TenderStatus = TenderStatus;
            copy.ProcedureInfo = ProcedureInfo;
            copy.Customer = Customer;
            return copy;
        }
    }
}
