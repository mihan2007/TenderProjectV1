using System;
using NotImplementedException = System.NotImplementedException;

namespace TenderProject.Model.BuisnessDomain
{
    public class ProcedureInfo
    {
        public string Number { get; set; }
        public string Law { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Stage { get; set; }
        public string SmallBusinessProcedure { get; set; }
        public string TradePlatformName { get; set; }
        public string TradePlatformSite { get; set; }
        public string ProcedureLink { get; set; }
        public string PublicationDate { get; set; }
        public string ApplicationDeadlineDate { get; set; }
        public string ApplicationDate { get; set; }
        public string AuctionDate { get; set; }
        public string SummarizingDate { get; set; }
        public string InitialPrice { get; set; }
        public string AplicationSecurityDeposit { get; set; }
        public string ContractSecurityDeposit { get; set; }
        public string QuataionPrice { get; set; }
        public string ResposablePersonContactInfo { get; set; }
        public string TradePlatformTarif { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryTime { get; set; }
        public string Comments { get; set; }


        public ProcedureInfo Copy()
        {
            return new ProcedureInfo
            {
                Number = Number,
                Law = Law,
                Type = Type,
                Subject = Subject,
                Stage = Stage,
                SmallBusinessProcedure = SmallBusinessProcedure,
                TradePlatformName = TradePlatformName,
                TradePlatformSite = TradePlatformSite,
                ProcedureLink = ProcedureLink,
                PublicationDate = PublicationDate,
                ApplicationDeadlineDate = ApplicationDeadlineDate,
                ApplicationDate = ApplicationDate,
                AuctionDate = AuctionDate,
                SummarizingDate = SummarizingDate,
                InitialPrice = InitialPrice,
                AplicationSecurityDeposit = AplicationSecurityDeposit,
                ContractSecurityDeposit = ContractSecurityDeposit,
                QuataionPrice = QuataionPrice,
                ResposablePersonContactInfo = ResposablePersonContactInfo,
                TradePlatformTarif = TradePlatformTarif,
                DeliveryAddress = DeliveryAddress,
                DeliveryTime = DeliveryTime,
                Comments = Comments
            };
        }
        protected bool Equals(ProcedureInfo other)
        {
            return Number == other.Number && Law == other.Law && Type == other.Type && Subject == other.Subject &&
                   Stage == other.Stage && SmallBusinessProcedure == other.SmallBusinessProcedure &&
                   TradePlatformName == other.TradePlatformName && TradePlatformSite == other.TradePlatformSite &&
                   ProcedureLink == other.ProcedureLink && PublicationDate == other.PublicationDate &&
                   ApplicationDeadlineDate == other.ApplicationDeadlineDate &&
                   ApplicationDate == other.ApplicationDate && AuctionDate == other.AuctionDate &&
                   SummarizingDate == other.SummarizingDate && InitialPrice == other.InitialPrice &&
                   AplicationSecurityDeposit == other.AplicationSecurityDeposit &&
                   ContractSecurityDeposit == other.ContractSecurityDeposit && QuataionPrice == other.QuataionPrice &&
                   ResposablePersonContactInfo == other.ResposablePersonContactInfo &&
                   TradePlatformTarif == other.TradePlatformTarif && DeliveryAddress == other.DeliveryAddress &&
                   DeliveryTime == other.DeliveryTime && Comments == other.Comments;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProcedureInfo)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Number);
            hashCode.Add(Law);
            hashCode.Add(Type);
            hashCode.Add(Subject);
            hashCode.Add(Stage);
            hashCode.Add(SmallBusinessProcedure);
            hashCode.Add(TradePlatformName);
            hashCode.Add(TradePlatformSite);
            hashCode.Add(ProcedureLink);
            hashCode.Add(PublicationDate);
            hashCode.Add(ApplicationDeadlineDate);
            hashCode.Add(ApplicationDate);
            hashCode.Add(AuctionDate);
            hashCode.Add(SummarizingDate);
            hashCode.Add(InitialPrice);
            hashCode.Add(AplicationSecurityDeposit);
            hashCode.Add(ContractSecurityDeposit);
            hashCode.Add(QuataionPrice);
            hashCode.Add(ResposablePersonContactInfo);
            hashCode.Add(TradePlatformTarif);
            hashCode.Add(DeliveryAddress);
            hashCode.Add(DeliveryTime);
            hashCode.Add(Comments);
            return hashCode.ToHashCode();
        }
    }



}
