using Presentation.Model.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Service.API;

namespace PresentationModel.Implementation;

internal class EventModelOperation : IEventModelOperation
{
    private IEventService _service;

    public EventModelOperation(IEventService? eventCrud = null)
    {
        _service = eventCrud ?? IEventService.CreateEventService();
    }

    private IEventModel Map(IEventService even)
    {
        return new EventModel(even.Id, even.StateId, even.UserId, even.CheckInDate, even.CheckOutDate, even.Type);
    }

    public async Task AddAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
    {
        await _service.AddEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
    }

    public async Task<IEventModel> GetAsync(int id)
    {
        return this.Map(await _service.GetEventAsync(id));
    }

    public async Task UpdateAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
    {
        await _service.UpdateEventAsync(id, stateId, userId, checkInDate, checkOutDate, type);
    }

    public async Task DeleteAsync(int id)
    {
        await _service.DeleteEventAsync(id);
    }

    public async Task<Dictionary<int, IEventModel>> GetAllAsync()
    {
        Dictionary<int, IEventModel> result = new Dictionary<int, IEventModel>();

        foreach (IEventService even in (await _service.GetAllEventsAsync()).Values)
        {
            result.Add(even.Id, Map(even));
        }

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        return await _service.GetEventsCountAsync();
    }

    public Task<IEnumerable<IEventModel>> GetEventsForUser(int userId)
    {
        throw new NotImplementedException();
    }
}
