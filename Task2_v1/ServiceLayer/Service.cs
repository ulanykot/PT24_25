using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2_v1;

namespace ServiceLayer
{
    internal class CatalogService : ICatalogService
    {
        private readonly string _connectionString;

        public CatalogService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Catalog GetCatalogById(int id)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                return context.Catalogs.SingleOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<Catalog> GetAllCatalogs()
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                return context.Catalogs.ToList();
            }
        }

        public void AddCatalog(Catalog catalog)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                context.Catalogs.InsertOnSubmit(catalog);
                context.SubmitChanges();
            }
        }

        public void UpdateCatalog(Catalog catalog)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                var existingCatalog = context.Catalogs.SingleOrDefault(c => c.Id == catalog.Id);
                if (existingCatalog != null)
                {
                    existingCatalog.RoomNumber = catalog.RoomNumber;
                    existingCatalog.RoomType = catalog.RoomType;
                    existingCatalog.IsBooked = catalog.IsBooked;
                    context.SubmitChanges();
                }
            }
        }

        public void DeleteCatalog(int id)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                var catalog = context.Catalogs.SingleOrDefault(c => c.Id == id);
                if (catalog != null)
                {
                    context.Catalogs.DeleteOnSubmit(catalog);
                    context.SubmitChanges();
                }
            }
        }
    }

    public class EventService : IEventService
    {
        private readonly string _connectionString;

        public EventService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Event GetEventById(int id)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                return context.Events.SingleOrDefault(e => e.Id == id);
            }
        }

        public IEnumerable<Event> GetAllEvents()
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                return context.Events.ToList();
            }
        }

        public void AddEvent(Event evnt)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                context.Events.InsertOnSubmit(evnt);
                context.SubmitChanges();
            }
        }

        public void UpdateEvent(Event evnt)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                var existingEvent = context.Events.SingleOrDefault(e => e.Id == evnt.Id);
                if (existingEvent != null)
                {
                    existingEvent.UserId = evnt.UserId;
                    existingEvent.StateId = evnt.StateId;
                    existingEvent.CheckInDate = evnt.CheckInDate;
                    existingEvent.CheckOutDate = evnt.CheckOutDate;
                    context.SubmitChanges();
                }
            }
        }

        public void DeleteEvent(int id)
        {
            using (var context = new HotelClassesDataContext(_connectionString))
            {
                var evnt = context.Events.SingleOrDefault(e => e.Id == id);
                if (evnt != null)
                {
                    context.Events.DeleteOnSubmit(evnt);
                    context.SubmitChanges();
                }
            }
        }
    }

    // Implement similar classes for State and User if needed
}
