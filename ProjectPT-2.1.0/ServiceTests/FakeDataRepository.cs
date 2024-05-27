using Data.API;
using Data.Database;

namespace ServiceTests;

internal class FakeDataRepository : IDataRepository
{
    public Dictionary<int, IUser> Users = new Dictionary<int, IUser>();

    public Dictionary<int, ICatalog> Products = new Dictionary<int, ICatalog>();

    public Dictionary<int, IEvent> Events = new Dictionary<int, IEvent>();

    public Dictionary<int, IState> States = new Dictionary<int, IState>();

    #region User CRUD

    public async Task AddUserAsync(int id, string name, string email)
    {
        this.Users.Add(id, new FakeUser(id, name, email));
    }

    public async Task<IUser> GetUserAsync(int id)
    {
        return await Task.FromResult(this.Users[id]);
    }

    public async Task UpdateUserAsync(int id, string name, string email, string userType)
    {
        this.Users[id].FirstName = name;
        this.Users[id].LastName = email;
        this.Users[id].UserType = userType;
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


    #region Product CRUD

    public async Task AddProductAsync(int id, string name, double price)
    {
        this.Products.Add(id, new FakeProduct(id, name, price));
    }

    public async Task<ICatalog> GetProductAsync(int id)
    {
        return await Task.FromResult(this.Products[id]);
    }

    public async Task UpdateProductAsync(int id, string name, double price)
    {
        this.Products[id].Name = name;
        this.Products[id].Price = price;
    }

    public async Task DeleteProductAsync(int id)
    {
        this.Products.Remove(id);
    }

    public async Task<Dictionary<int, ICatalog>> GetAllProductsAsync()
    {
        return await Task.FromResult(this.Products);
    }

    public async Task<int> GetProductsCountAsync()
    {
        return await Task.FromResult(this.Products.Count);
    }

    #endregion


    #region State CRUD

    public async Task AddStateAsync(int id, int productId, int productQuantity)
    {
        this.States.Add(id, new FakeState(id, productId, productQuantity));
    }

    public async Task<IState> GetStateAsync(int id)
    {
        return await Task.FromResult(this.States[id]);
    }

    public async Task UpdateStateAsync(int id, int productId, int productQuantity)
    {
        this.States[id].productId = productId;
        this.States[id].productQuantity = productQuantity;
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

    public async Task AddEventAsync(int id, int stateId, int userId, string type, int quantity = 0)
    {
        IUser user = await this.GetUserAsync(userId);
        IState state = await this.GetStateAsync(stateId);
        ICatalog catalog = await this.GetProductAsync(state.productId);

        switch (type)
        {
            case "CheckIn":
                if (catalog.isBooked == true)
                    throw new Exception("Room unavailable, please check later!");
                await UpdateProductAsync((int)state.RoomCatalogId, catalog.RoomNumber, catalog.RoomType, catalog.isBooked = true);
                await UpdateUserAsync(userId, user.FirstName, user.LastName, user.UserType);
                break;

            case "CheckOut":
                if (catalog.isBooked == false)
                    throw new Exception("Room is not even booked!");

                await UpdateProductAsync((int)state.RoomCatalogId, catalog.RoomNumber, catalog.RoomType, false);
                await UpdateUserAsync(userId, user.FirstName, user.LastName, user.UserType);

                break;


            default:
                throw new Exception("This event type does not exist!");
        }

        this.Events.Add(id, new FakeEvent(id, stateId, userId, type, quantity));
    }

    public async Task<IEvent> GetEventAsync(int id)
    {
        return await Task.FromResult(this.Events[id]);
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime dateTime, DateTime checkOut, string type, int? quantity)
    {
        ((FakeEvent)this.Events[id]).stateId = stateId;
        ((FakeEvent)this.Events[id]).userId = userId;
        ((FakeEvent)this.Events[id]).occurrenceDate = checkOut;
        ((FakeEvent)this.Events[id]).Type = type;
        ((FakeEvent)this.Events[id]).Quantity = quantity ?? ((FakeEvent)this.Events[id]).Quantity;
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

    #endregion
}
