using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IEventService
    {
        static IEventService CreateEventService(IDataRepository? dataRepository = null)
        {
            return new EventService(dataRepository ?? IDataRepository.CreateDatabase());
        }

        int Id { get; set; }

        int StateId { get; set; }

        int UserId { get; set; }

        DateTime CheckInDate { get; set; }

        DateTime CheckOutDate { get; set; }

        string Type { get; set; }

        Task AddEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

        Task<IEventService> GetEventAsync(int id);

        Task UpdateEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

        Task DeleteEventAsync(int id);

        Task<Dictionary<int, IEventService>> GetAllEventsAsync();

        Task<int> GetEventsCountAsync();
    }
}
