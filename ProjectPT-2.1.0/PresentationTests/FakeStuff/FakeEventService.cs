using Service.API;

namespace PresentationTests;

internal class FakeEventService : IEventService
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public int Id { get; set; }
    public int StateId { get; set; }
    public int UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Type { get; set; }

    public FakeEventService(int id, int stateId, int userId, DateTime checkIn, DateTime checkOut, string type)
    {
        this.Id = id;
        this.StateId = stateId;
        this.UserId = userId;
        this.CheckInDate = checkIn;
        this.CheckOutDate = checkOut;
        this.Type = type;
    }
    public FakeEventService()
    {

    }

    public async Task AddEventAsync(int id, int stateId, int userId, DateTime CheckInDate, DateTime CheckOutDate, string Type)
    {
        await this._fakeRepository.AddEventAsync(id, stateId, userId, CheckInDate, CheckOutDate, Type);
    }

    public async Task<IEventService> GetEventAsync(int id)
    {
        return await this._fakeRepository.GetEventAsync(id);
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime CheckInDate, DateTime CheckOutDate, string Type)
    {
        await this._fakeRepository.UpdateEventAsync(id, stateId, userId, CheckInDate, CheckOutDate, Type);
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


}
