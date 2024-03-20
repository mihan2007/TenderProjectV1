using System.Collections.Generic;
using TenderProject.Model.BuisnessDomain;

namespace TenderProject
{
    public abstract class DataProviderBase
    {
       public abstract List<TenderInfo> Get(string path);
       public abstract void Set(string path, List<TenderInfo> tenders);

        protected bool Check()
        {
            return true;
        }

    }
}
