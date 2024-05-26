using DataLayer.API;
using DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation
{
    public static class DataContextFactory
    {
        public static IDataContext CreateContext()
        {
            return CreateContext();
        }
        public static IDataContext CreateContext(string connectionString)
        {
            return new HotelClassesDataContext(connectionString);
        }

    }
}
