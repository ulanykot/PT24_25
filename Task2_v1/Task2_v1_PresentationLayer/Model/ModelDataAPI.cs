using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace Task2_v1_PresentationLayer.Model
{
    public abstract class ModelDataAPI
    {
        public static ModelDataAPI Create(IUserService userService, ICatalogService catalogService, IEventService eventService, IStateService stateService)
        {
            return new ModelData(userService, catalogService, eventService, stateService);
        }

        public static ModelDataAPI Create()
        {
            return new ModelData();
        }

        // User operations
        public abstract void AddUser(User user);
        public abstract User GetUser(int id);
        public abstract void RemoveUser(int id);
        public abstract IEnumerable<User> GetAllUsers();

        // Catalog operations
        public abstract void AddCatalog(Catalog catalog);
        public abstract Catalog GetCatalog(int id);
        public abstract IEnumerable<Catalog> GetAllCatalogs();
        public abstract void RemoveCatalog(int id);

        // Event operations
        public abstract void AddEvent(Event eve);
        public abstract Event GetEvent(int id);
        public abstract IEnumerable<Event> GetAllEvents();
        public abstract void RemoveEvent(int id);

        // State operations
        public abstract void AddState(State state);
        public abstract State GetState(int id);
        public abstract IEnumerable<State> GetAllStates();
        public abstract void RemoveState(int id);

    }

}

