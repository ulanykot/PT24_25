using Data.Implementation;

namespace Data.API;

public interface IDataContext
{
    static IDataContext CreateContext(string? connectionString = null)
    {
        return new DataContext(connectionString);
    }

    #region User CRUD

    Task AddUserAsync(IUser user);

    Task<IUser> GetUserAsync(int id);

    Task UpdateUserAsync(IUser user);

    Task DeleteUserAsync(int id);

    Task<Dictionary<int, IUser>> GetAllUsersAsync();

    Task<int> GetUsersCountAsync();

    #endregion User CRUD


    #region Catalog CRUD

    Task AddCatalogAsync(ICatalog catalog);

    Task<ICatalog> GetCatalogAsync(int id);

    Task UpdateCatalogAsync(ICatalog catalog);

    Task DeleteCatalogAsync(int id);

    Task<Dictionary<int, ICatalog>> GetAllCatalogsAsync();

    Task<int> GetCatalogsCountAsync();

    #endregion


    #region State CRUD

    Task AddStateAsync(IState state);

    Task<IState> GetStateAsync(int id);

    Task UpdateStateAsync(IState state);

    Task DeleteStateAsync(int id);

    Task<Dictionary<int, IState>> GetAllStatesAsync();

    Task<int> GetStatesCountAsync();

    #endregion


    #region Event CRUD

    Task AddEventAsync(IEvent even);

    Task<IEvent> GetEventAsync(int id);

    Task UpdateEventAsync(IEvent even);

    Task DeleteEventAsync(int id);

    Task<Dictionary<int, IEvent>> GetAllEventsAsync();

    Task<int> GetEventsCountAsync();
    Task<IEnumerable<IEvent>> GetEventsForUser(int userId);

    #endregion


    #region Utils

    Task<bool> CheckIfUserExists(int id);

    Task<bool> CheckIfCatalogExists(int id);

    Task<bool> CheckIfStateExists(int id);

    Task<bool> CheckIfEventExists(int id, string type);

    #endregion
}
