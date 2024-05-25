using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Database;

namespace Task2_v1_PresentationLayer.Model
{
    public abstract class ModelDataAPI
    {
        public static ModelDataAPI Create() => new ModelData();

        public abstract void AddUser(User user);
        public abstract User GetUser(int id);
        public abstract void RemoveUser(int id);
        public abstract IEnumerable<User> GetAllUsers();
        public abstract  Task<IEnumerable<Event>> GetEventsForUser(int userId);

        public abstract void AddCatalog(Catalog catalog);
        public abstract Catalog GetCatalog(int id);
        public abstract IEnumerable<Catalog> GetAllCatalogs();
        public abstract void RemoveCatalog(int id);

        public abstract void AddEvent(Event eve);
        public abstract Event GetEvent(int id);
        public abstract IEnumerable<Event> GetAllEvents();
        public abstract void RemoveEvent(int id);

        public abstract void AddState(State state);
        public abstract State GetState(int id);
        public abstract IEnumerable<State> GetAllStates();
        public abstract void RemoveState(int id);

        public event EventHandler DataChanged;

        protected void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}

