using DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public partial class HotelClassesDataContext : API.IDataContext
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\HotelDatabase.mdf;Integrated Security=False";

        #region user
        #endregion user
        public Task<IEnumerable<Event>> GetEventsForUser(int userId)
        {
            return Task.Run(() =>
            {
                using (var context = new HotelClassesDataContext(_connectionString))
                {
                    return context.Events.Where(e => e.UserId == userId).ToList() as IEnumerable<Event>;
                }
            });
        }
    }
}
