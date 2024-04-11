
using System;

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
            copy.ProcedureInfo = ProcedureInfo.Copy();
            copy.Customer = Customer.Copy();
            return copy;
        }

        public void Reinit(TenderInfo source)
        {
            Id = source.Id;
            FilePath = source.FilePath;
            TenderStatus = source.TenderStatus;
            ProcedureInfo = source.ProcedureInfo;
            Customer = source.Customer;
        }

        protected bool Equals(TenderInfo other)
        {
            return Id == other.Id && FilePath == other.FilePath && TenderStatus == other.TenderStatus &&
                   Equals(ProcedureInfo, other.ProcedureInfo) && Equals(Customer, other.Customer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TenderInfo)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FilePath, TenderStatus, ProcedureInfo, Customer);
        }
    }
}
