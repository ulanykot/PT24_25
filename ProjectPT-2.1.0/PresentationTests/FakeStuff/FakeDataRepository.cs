using PresentationTest;
using Service.API;

namespace PresentationTests;

internal class FakeDataRepository
{
    public Dictionary<int, IUserService> Users = new Dictionary<int, IUserService>();

    public Dictionary<int, ICatalogService> Catalogs = new Dictionary<int, ICatalogService>();

    public Dictionary<int, IEventService> Events = new Dictionary<int, IEventService>();

    public Dictionary<int, IStateService> States = new Dictionary<int, IStateService>();

    #region User CRUD

    public async Task AddUserAsync(int id, string FirstName, string LastName, string UserType)
    {
        this.Users.Add(id, new FakeUserService(id, FirstName, LastName, UserType));
    }

    public async Task<IUserService> GetUserAsync(int id)
    {
        return await Task.FromResult(this.Users[id]);
    }

    public async Task UpdateUserAsync(int id, string FirstName, string LastName, string UserType)
    {
        this.Users[id].FirstName = FirstName;
        this.Users[id].LastName = LastName;
        this.Users[id].UserType = UserType;
    }

    public async Task DeleteUserAsync(int id)
    {
        this.Users.Remove(id);
    }

    public async Task<Dictionary<int, IUserService>> GetAllUsersAsync()
    {
        return await Task.FromResult(this.Users);
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await Task.FromResult(this.Users.Count);
    }

    public bool CheckIfUserExists(int id)
    {
        return this.Users.ContainsKey(id);
    }

    #endregion User CRUD


    #region Catalog CRUD

    public async Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        this.Catalogs.Add(id, new FakeCatalogService(id, roomNumber, roomType, isBooked));
    }

    public async Task<ICatalogService> GetCatalogAsync(int id)
    {
        return await Task.FromResult(this.Catalogs[id]);
    }

    public async Task UpdateCatalogAsync(int id, int number, string type, bool isBooked)
    {
        this.Catalogs[id].RoomType = type;
        this.Catalogs[id].RoomNumber = number;
        this.Catalogs[id].isBooked = isBooked;
    }

    public async Task DeleteCatalogAsync(int id)
    {
        this.Catalogs.Remove(id);
    }

    public async Task<Dictionary<int, ICatalogService>> GetAllCatalogAsync()
    {
        return await Task.FromResult(this.Catalogs);
    }

    public async Task<int> GetCatalogsCountAsync()
    {
        return await Task.FromResult(this.Catalogs.Count);
    }

    #endregion


    #region State CRUD

    public async Task AddStateAsync(int id, int roomId, int price)
    {
        this.States.Add(id, new FakeStateService(id, roomId, price));
    }

    public async Task<IStateService> GetStateAsync(int id)
    {
        return await Task.FromResult(this.States[id]);
    }

    public async Task UpdateStateAsync(int id, int roomId, int price)
    {
        this.States[id].RoomCatalogId = roomId;
        this.States[id].Price = price;
    }

    public async Task DeleteStateAsync(int id)
    {
        this.States.Remove(id);
    }

    public async Task<Dictionary<int, IStateService>> GetAllStatesAsync()
    {
        return await Task.FromResult(this.States);
    }

    public async Task<int> GetStatesCountAsync()
    {
        return await Task.FromResult(this.States.Count);
    }

    #endregion


    #region Event CRUD

    public async Task AddEventAsync(int id, int stateId, int userId, DateTime CheckInDate, DateTime CheckOutDate, string Type)
    {
        IUserService user = await this.GetUserAsync(userId);
        IStateService state = await this.GetStateAsync(stateId);

        this.Events.Add(id, new FakeEventService(id, stateId, userId, CheckInDate, CheckOutDate, Type));
    }

    public async Task<IEventService> GetEventAsync(int id)
    {
        return await Task.FromResult(this.Events[id]);
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime CheckInDate, DateTime CheckOutDate, string type)
    {
        this.Events[id].StateId = stateId;
        this.Events[id].UserId = userId;
        this.Events[id].CheckInDate = CheckInDate;
        this.Events[id].CheckOutDate = CheckOutDate;
        this.Events[id].Type = type;
    }

    public async Task DeleteEventAsync(int id)
    {
        this.Events.Remove(id);
    }

    public async Task<Dictionary<int, IEventService>> GetAllEventsAsync()
    {
        return await Task.FromResult(this.Events);
    }

    public async Task<int> GetEventsCountAsync()
    {
        return await Task.FromResult(this.Events.Count);
    }

    #endregion
}
