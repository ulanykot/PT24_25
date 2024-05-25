using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;

namespace ServiceLayer
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUser(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);
        IEnumerable<User> GetAllUsers();
        UserService CreateUserService(string _connectionString);
        Task<IEnumerable<Event>> GetEventsForUser(int userId);
    }

    public interface IStateService
    {
        void AddState(State state);
        State GetState(int id);
        IEnumerable<State> GetAllStates();
        void DeleteState(int id);
        StateService CreateStateService(string _connectionString);

    }

    public interface ICatalogService
    {
        Catalog GetCatalogById(int id);
        IEnumerable<Catalog> GetAllCatalogs();
        void AddCatalog(Catalog catalog);
        void UpdateCatalog(Catalog catalog);
        void DeleteCatalog(int id);
        CatalogService CreateCatalogService(string _connectionString);
    }

    public interface IEventService
    {
        Event GetEventById(int id);
        IEnumerable<Event> GetAllEvents();
        void AddEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void DeleteEvent(int id);
        EventService CreateEventService(string _connectionString);
    }

}
