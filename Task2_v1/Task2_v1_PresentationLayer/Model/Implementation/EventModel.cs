using DataLayer.API;
using ServiceLayer.API;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model.Implementation
{
    internal class EventModel : IEventModel
    {
        IEventService _service;
        public EventModel(IEventService service = null)
        {
            _service = service ?? ServiceFactory.CreateEventService();
        }
        public async Task AddAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
        {
            await this._service.AddEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
        }

        public async Task DeleteAsync(int id)
        {
            await this._service.DeleteEventAsync(id);
        }

        public async Task<Dictionary<int, IEventService>> GetAllAsync()
        {
            return await this._service.GetAllEventsAsync();
        }

        public async Task<IEventService> GetAsync(int id)
        {
            return await this._service.GetEventAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await this._service.GetEventsCountAsync();
        }

        public async Task<IEnumerable<IEventService>> GetEventsForUser(int userId)
        {
            return await _service.GetEventsForUser(userId);
        }

        public async Task UpdateAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
        {
            await this._service.UpdateEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
        }
    }
}
