using PresentationTest;
using Service.API;

namespace PresentationTests;

internal class FakeDataRepository
{
    public Dictionary<int, IUserService> Users = new Dictionary<int, IUserService>();

    public Dictionary<int, ICatalogService> Products = new Dictionary<int, ICatalogService>();

    public Dictionary<int, IEventService> Events = new Dictionary<int, IEventService>();

    public Dictionary<int, IStateService> States = new Dictionary<int, IStateService>();

    #region User CRUD

    public async Task AddUserAsync(int id, int roomNumber, string roomType, bool isBooked)
    {
        this.Users.Add(id, new FakeUserService(id, roomNumber, roomType, isBooked));
    }

    public async Task<IUserService> GetUserAsync(int id)
    {
        return await Task.FromResult(this.Users[id]);
    }

    public async Task UpdateUserAsync(int id, string name, string email)
    {
        this.Users[id].Name = name;
        this.Users[id].Email = email;
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


    #region Product CRUD

    public async Task AddProductAsync(int id, string name, double price)
    {
        this.Products.Add(id, new FakeProductService(id, name, price));
    }

    public async Task<ICatalogService> GetProductAsync(int id)
    {
        return await Task.FromResult(this.Products[id]);
    }

    public async Task UpdateProductAsync(int id, string type, int number, bool isBooked)
    {
        this.Products[id].RoomType = type;
        this.Products[id].RoomNumber = number;
        this.Products[id].isBooked = isBooked;
    }

    public async Task DeleteProductAsync(int id)
    {
        this.Products.Remove(id);
    }

    public async Task<Dictionary<int, ICatalogService>> GetAllProductsAsync()
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
        this.States.Add(id, new FakeStateService(id, productId, productQuantity));
    }

    public async Task<IStateService> GetStateAsync(int id)
    {
        return await Task.FromResult(this.States[id]);
    }

    public async Task UpdateStateAsync(int id, int productId, int price)
    {
        this.States[id].RoomCatalogId = productId;
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

    public async Task AddEventAsync(int id, int stateId, int userId, string type, int quantity = 0)
    {
        IUserService user = await this.GetUserAsync(userId);
        IStateService state = await this.GetStateAsync(stateId);
        ICatalogService product = await this.GetProductAsync(state.productId);

        switch (type)
        {
            case "PurchaseEvent":
                
                if (state.productQuantity == 0)
                    throw new Exception("Product unavailable, please check later!");

                await this.UpdateStateAsync(stateId, product.Id, state.productQuantity - 1);
                await this.UpdateUserAsync(userId, user.Name, user.Email);

                break;

            case "ReturnEvent":
                Dictionary<int, IEventDTO> events = await this.GetAllEventsAsync();
                Dictionary<int, IStateDTO> states = await this.GetAllStatesAsync();

                int copiesBought = 0;

                foreach
                (
                    IEventDTO even in
                    from even in events.Values
                    from stat in states.Values
                    where even.userId == user.Id &&
                          even.stateId == stat.Id &&
                          stat.productId == product.Id
                    select even
                )
                    if (((FakeEventDTO)even).Type == "PurchaseEvent")
                        copiesBought++;
                    else if (((FakeEventDTO)even).Type == "ReturnEvent")
                        copiesBought--;

                copiesBought--;

                if (copiesBought < 0)
                    throw new Exception("You do not own this product!");

                await this.UpdateStateAsync(stateId, product.Id, state.productQuantity + 1);
                await this.UpdateUserAsync(userId, user.Name, user.Email);

                break;

            case "SupplyEvent":
                if (quantity <= 0)
                    throw new Exception("Can not supply with this amount!");

                await this.UpdateStateAsync(stateId, product.Id, state.productQuantity + quantity);

                break;
        }

        this.Events.Add(id, new FakeEventDTO(id, stateId, userId, type, quantity));
    }

    public async Task<IEventDTO> GetEventAsync(int id)
    {
        return await Task.FromResult(this.Events[id]);
    }

    public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurrenceDate, string type, int? quantity)
    {
        ((FakeEventDTO)this.Events[id]).stateId = stateId;
        ((FakeEventDTO)this.Events[id]).userId = userId;
        ((FakeEventDTO)this.Events[id]).occurrenceDate = occurrenceDate;
        ((FakeEventDTO)this.Events[id]).Type = type;
        ((FakeEventDTO)this.Events[id]).Quantity = quantity ?? ((FakeEventDTO)this.Events[id]).Quantity;
    }

    public async Task DeleteEventAsync(int id)
    {
        this.Events.Remove(id);
    }

    public async Task<Dictionary<int, IEventDTO>> GetAllEventsAsync()
    {
        return await Task.FromResult(this.Events);
    }

    public async Task<int> GetEventsCountAsync()
    {
        return await Task.FromResult(this.Events.Count);
    }

    #endregion
}
