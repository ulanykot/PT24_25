using Data.Implementation;

namespace Data.API;

public interface IDataRepository
{
    static IDataRepository CreateDatabase(IDataContext? dataContext = null)
    {
        return new DataRepository(dataContext ?? new DataContext());
    }
    #region User CRUD

    Task AddUserAsync(int id, string firstName, string lastName, string userType);

    Task<IUser> GetUserAsync(int id);

    Task UpdateUserAsync(int id, string firstName, string lastName, string userType);

    Task DeleteUserAsync(int id);

    Task<Dictionary<int, IUser>> GetAllUsersAsync();

    Task<int> GetUsersCountAsync();

    #endregion User CRUD


    #region Catalog CRUD

    Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task<ICatalog> GetCatalogAsync(int id);

    Task UpdateCatalogAsync(int id, int roomNumber, string roomType, bool isBooked);

    Task DeleteCatalogAsync(int id);

    Task<Dictionary<int, ICatalog>> GetAllCatalogsAsync();

    Task<int> GetCatalogsCountAsync();

    #endregion


    #region State CRUD

    Task AddStateAsync(int id, int roomId, int price);

    Task<IState> GetStateAsync(int id);

    Task UpdateStateAsync(int id, int roomId, int price);

    Task DeleteStateAsync(int id);

    Task<Dictionary<int, IState>> GetAllStatesAsync();

    Task<int> GetStatesCountAsync();

    #endregion


    #region Event CRUD

    Task AddEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

    Task<IEvent> GetEventAsync(int id);

    Task UpdateEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type);

    Task DeleteEventAsync(int id);

    Task<Dictionary<int, IEvent>> GetAllEventsAsync();

    Task<int> GetEventsCountAsync();

    #endregion
}
