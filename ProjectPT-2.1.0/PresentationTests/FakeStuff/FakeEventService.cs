using Service.API;

namespace PresentationTests;

internal class FakeEventService : IEventService
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public int Id { get ; set ; }
    public int StateId { get ; set ; }
    public int UserId { get ; set ; }
    public DateTime CheckInDate { get ; set ; }
    public DateTime CheckOutDate { get ; set ; }
    public string Type { get ; set ; }

    public FakeEventService()
    {

    }
    
    public async Task AddEventAsync(int id, int stateId, int userId, string type, int quantity = 0)
    {
        await this._fakeRepository.AddEventAsync(id, stateId, userId, type, quantity);
    }

    public async Task<IEventService> GetEventAsync(int id)
    {
        return await this._fakeRepository.GetEventAsync(id);
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurrenceDate, string type, int? quantity)
    {
        await this._fakeRepository.UpdateEventAsync(id, stateId, userId, occurrenceDate, type, quantity);
    }

    public async Task DeleteEventAsync(int id)
    {
        await this._fakeRepository.DeleteEventAsync(id);
    }

    public async Task<Dictionary<int, IEventService>> GetAllEventsAsync()
    {
        Dictionary<int, IEventService> result = new Dictionary<int, IEventService>();

        foreach (IEventService even in (await this._fakeRepository.GetAllEventsAsync()).Values)
        {
            result.Add(even.Id, even);
        }

        return result;
    }

    public async Task<int> GetEventsCountAsync()
    {
        return await this._fakeRepository.GetEventsCountAsync();
    }

    public Task AddEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
    {
        throw new NotImplementedException();
    }

    Task<IEventService> IEventService.GetEventAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
    {
        throw new NotImplementedException();
    }

    Task<Dictionary<int, IEventService>> IEventService.GetAllEventsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IEventService>> GetEventsForUser(int userId)
    {
        throw new NotImplementedException();
    }
}
