using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2_v1;

namespace ServiceLayer
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUser(int id);
        void RemoveUser(int id);
        List<User> GetAllUsers();
    }

    public interface IStateService
    {
        void AddState(State state);
        State GetState(int id);
        List<State> GetAllStates();
        void RemoveState(int id);
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
