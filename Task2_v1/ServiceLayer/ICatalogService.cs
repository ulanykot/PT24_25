using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace ServiceLayer
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUser(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);
        IEnumerable<User> GetAllUsers();
    }

    public interface IStateService
    {
        void AddState(State state);
        State GetState(int id);
        IEnumerable<State> GetAllStates();
        void DeleteState(int id);
    }

    public interface ICatalogService
    {
        Catalog GetCatalogById(int id);
        IEnumerable<Catalog> GetAllCatalogs();
        void AddCatalog(Catalog catalog);
        void UpdateCatalog(Catalog catalog);
        void DeleteCatalog(int id);
    }

    public interface IEventService
    {
        Event GetEventById(int id);
        IEnumerable<Event> GetAllEvents();
        void AddEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void DeleteEvent(int id);
    }

}
