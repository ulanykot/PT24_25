using Presentation.Model.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Service.API;

namespace Presentation.Model.Implementation;

internal class EventModelOperation : IEventModelOperation
{
    private IEventService _service;

    public EventModelOperation(IEventService? eventCrud = null)
    {
        this._service = eventCrud ?? IEventService.CreateEventService();
    }

    private IEventModel Map(IEventService even)
    {
        return new EventModel(even.Id, even.StateId, even.UserId, even.CheckInDate, even.CheckOutDate, even.Type);
    }

    public async Task AddAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
    {
        await this._service.AddEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
    }

    public async Task<IEventModel> GetAsync(int id)
    {
        return this.Map(await this._service.GetEventAsync(id));
    }

    public async Task UpdateAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
    {
        await this._service.UpdateEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
    }

    public async Task DeleteAsync(int id)
    {
        await this._service.DeleteEventAsync(id);
    }

    public async Task<Dictionary<int, IEventModel>> GetAllAsync()
    {
        Dictionary<int, IEventModel> result = new Dictionary<int, IEventModel>();

        foreach (IEventService even in (await this._service.GetAllEventsAsync()).Values)
        {
            result.Add(even.Id, this.Map(even));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await this._service.GetEventsCountAsync();
    }

    public Task<IEnumerable<IEventModel>> GetEventsForUser(int userId)
    {
        throw new NotImplementedException();
    }
}
