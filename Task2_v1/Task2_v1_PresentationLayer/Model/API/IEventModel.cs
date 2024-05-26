using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public interface IEventModel
    {
        Task AddAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

        Task<IEventService> GetAsync(int id);

        Task UpdateAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

        Task DeleteAsync(int id);

        Task<Dictionary<int, IEventService>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<IEnumerable<IEventService>> GetEventsForUser(int userId);
    }
}
