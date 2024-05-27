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
        private static readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database\\HotelDatabase.mdf;Integrated Security=False";
        public static IDataContext CreateContext()
        {
            return new HotelClassesDataContext(connectionString);
        }
        public static IDataContext CreateContext(string connectionString)
        {
            return new HotelClassesDataContext(connectionString);
        }

    }
}
