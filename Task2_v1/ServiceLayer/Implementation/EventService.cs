using DataLayer.API;
using DataLayer.Implementation;
using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    internal class EventService : IEventService
    {
        IDataRepository _repository;
        public EventService(IDataRepository repository)
        {
            this._repository = repository;
        }
        public int Id { get ; set ; }
        public int? StateId { get ; set ; }
        public int? UserId { get ; set ; }
        public DateTime? CheckInDate { get ; set ; }
        public DateTime? CheckOutDate { get ; set ; }
        public string Type { get ; set ; }

        public EventService(int id, int? stateId, int? userId, DateTime? checkIn, DateTime? checkOut, string type)
        {
            this.Id = id;
            this.StateId = stateId;
            this.UserId = userId;
            this.CheckInDate = checkIn;
            this.CheckOutDate = checkOut;
            this.Type = type;
        }

        private IEventService Map(IEvent even)
        {
            return new EventService(even.Id, even.StateId, even.UserId, even.CheckInDate, even.CheckOutDate, even.Type);
        }
        public async Task AddEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
        {
            await this._repository.AddEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
        }

        public async Task DeleteEventAsync(int id)
        {
            await this._repository.DeleteEventAsync(id);
        }

        public async Task<Dictionary<int, IEventService>> GetAllEventsAsync()
        {
            Dictionary<int, IEventService> result = new Dictionary<int, IEventService>();

            foreach (IEvent even in (await this._repository.GetAllEventsAsync()).Values)
            {
                result.Add(even.Id, this.Map(even));
            }

            return result;
        }

        public async Task<IEventService> GetEventAsync(int id)
        {
            return this.Map(await this._repository.GetEventAsync(id));
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await this._repository.GetEventsCountAsync();
        }

        public Task<IEnumerable<IEventService>> GetEventsForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
        {
            await this._repository.UpdateEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
        }
    }
}
