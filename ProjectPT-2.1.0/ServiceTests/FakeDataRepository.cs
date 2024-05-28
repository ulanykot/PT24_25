using Data.API;
using Data.Database;

namespace ServiceTests;

internal class FakeDataRepository : IDataRepository
{
    public Dictionary<int, IUser> Users = new Dictionary<int, IUser>();

    public Dictionary<int, ICatalog> Catalogs = new Dictionary<int, ICatalog>();

    public Dictionary<int, IEvent> Events = new Dictionary<int, IEvent>();

    public Dictionary<int, IState> States = new Dictionary<int, IState>();

    #region User CRUD

    public async Task AddUserAsync(int id, string firstName, string lastName, string UserType)
    {
        this.Users.Add(id, new FakeUser(id, firstName, lastName, UserType));
    }

    public async Task<IUser> GetUserAsync(int id)
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

    public async Task<Dictionary<int, IUser>> GetAllUsersAsync()
    {
        return await Task.FromResult(this.Users);
    }

    public async Task<int> GetUsersCountAsync()
    {
        return await Task.FromResult(this.Users.Count);
    }

    #endregion User CRUD


    #region Catalog CRUD

    public async Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        this.Catalogs.Add(id, new FakeCatalog(id, roomNumber, roomType, isBooked));
    }

    public async Task<ICatalog> GetCatalogAsync(int id)
    {
        return await Task.FromResult(this.Catalogs[id]);
    }

    public async Task UpdateCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        this.Catalogs[id].RoomNumber = roomNumber;
        this.Catalogs[id].RoomType = roomType;
        this.Catalogs[id].isBooked = isBooked;
    }

    public async Task DeleteCatalogAsync(int id)
    {
        this.Catalogs.Remove(id);
    }

    public async Task<Dictionary<int, ICatalog>> GetAllCatalogsAsync()
    {
        return await Task.FromResult(this.Catalogs);
    }

    public async Task<int> GetCatalogsCountAsync()
    {
        return await Task.FromResult(this.Catalogs.Count);
    }

    #endregion


    #region State CRUD

    public async Task AddStateAsync(int id, int roomCatalogId, int price)
    {
        this.States.Add(id, new FakeState(id, roomCatalogId, price));
    }

    public async Task<IState> GetStateAsync(int id)
    {
        return await Task.FromResult(this.States[id]);
    }

    public async Task UpdateStateAsync(int id, int roomCatalogId, int price)
    {
        this.States[id].RoomCatalogId = roomCatalogId;
        this.States[id].Price = price;
    }

    public async Task DeleteStateAsync(int id)
    {
        this.States.Remove(id);
    }

    public async Task<Dictionary<int, IState>> GetAllStatesAsync()
    {
        return await Task.FromResult(this.States);
    }

    public async Task<int> GetStatesCountAsync()
    {
        return await Task.FromResult(this.States.Count);
    }

    #endregion


    #region Event CRUD

    public async Task AddEventAsync(int id, int stateId, int userId, DateTime checkIn, DateTime checkOut, string type)
    {
        IUser user = await this.GetUserAsync(userId);
        IState state = await this.GetStateAsync(stateId);
        ICatalog catalog = await this.GetCatalogAsync(state.RoomCatalogId);

        switch (type)
        {
            case "CheckIn":
                if (catalog.isBooked == true)
                    throw new Exception("Room unavailable, please check later!");
                await UpdateCatalogAsync((int)state.RoomCatalogId, catalog.RoomNumber, catalog.RoomType, catalog.isBooked = true);
                await UpdateUserAsync(userId, user.FirstName, user.LastName, user.UserType);
                break;

            case "CheckOut":
                if (catalog.isBooked == false)
                    throw new Exception("Room is not even booked!");

                await UpdateCatalogAsync((int)state.RoomCatalogId, catalog.RoomNumber, catalog.RoomType, false);
                await UpdateUserAsync(userId, user.FirstName, user.LastName, user.UserType);

                break;


            default:
                throw new Exception("This event type does not exist!");
        }

        this.Events.Add(id, new FakeEvent(id, stateId, userId, checkIn, checkOut, type));
    }

    public async Task<IEvent> GetEventAsync(int id)
    {
        return await Task.FromResult(this.Events[id]);
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime checkIn, DateTime checkOut, string type)
    {
        ((FakeEvent)this.Events[id]).StateId = stateId;
        ((FakeEvent)this.Events[id]).UserId = userId;
        ((FakeEvent)this.Events[id]).CheckInDate = checkIn;
        ((FakeEvent)this.Events[id]).CheckOutDate = checkOut;
        ((FakeEvent)this.Events[id]).Type = type;
    }

    public async Task DeleteEventAsync(int id)
    {
        this.Events.Remove(id);
    }

    public async Task<Dictionary<int, IEvent>> GetAllEventsAsync()
    {
        return await Task.FromResult(this.Events);
    }

    public async Task<int> GetEventsCountAsync()
    {
        return await Task.FromResult(this.Events.Count);
    }

    public Task<IEnumerable<IEvent>> GetEventsForUser(int userId)
    {
        throw new NotImplementedException();
    }

    #endregion
}
