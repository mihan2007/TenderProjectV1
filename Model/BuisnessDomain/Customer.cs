using System;
using NotImplementedException = System.NotImplementedException;

namespace TenderProject.Model.BuisnessDomain
{
    public class Customer
    {
        public string Name { get; set; }
        public string PostAdress { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string OGRN { get; set; }
        public string CustomerContactInfo { get; set; }

        public Customer Copy()
        {
            var copy = new Customer();
            copy.Name = Name;
            copy.PostAdress = PostAdress;
            copy.INN = INN;
            copy.KPP = KPP;
            copy.OGRN = OGRN;
            copy.CustomerContactInfo = CustomerContactInfo;
            return copy;
        }

        protected bool Equals(Customer other)
        {
            return Name == other.Name && PostAdress == other.PostAdress && INN == other.INN && KPP == other.KPP &&
                   OGRN == other.OGRN && CustomerContactInfo == other.CustomerContactInfo;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Customer)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, PostAdress, INN, KPP, OGRN, CustomerContactInfo);
        }
    }
}
