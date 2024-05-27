using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation
{
    public static class DataRepositoryFactory
    {
        public static IDataRepository CreateDatabase(IDataContext dataContext = null)
        {
            if (dataContext == null)
            {
                dataContext = DataContextFactory.CreateContext();
            }

            return new DataRepository(dataContext);
        }
    }
}
