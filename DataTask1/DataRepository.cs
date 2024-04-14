using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataTask1
{
    public class DataRepository
    {
        private DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
        }
        #region Users
        public void AddUser(User user)
        {
            context.users.Add(user);
        }
        public User GetUser(int id)
        {
            foreach(var user in context.users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            throw new Exception("No user found.");
        }
        public void RemoveUser(int id)
        {
            User tempUser = GetUser(id);

            foreach (var user in context.users)
            {
                if (user.Id == id)
                {
                    context.users.Remove(user);
                }
            }
        }
        //for later implementation
        public void UpdateUser(User user)
        {
               
        }
        public List<User> GetAllUsers()
        {
            return context.users;
        }
        #endregion
        #region Catalog
        private int counter = 0;
        public void AddCatalog(Catalog catalog)
        {
            context.catalogs.Add(counter, catalog);
            counter++;
        }
        public Catalog GetCatalog(int id)
        {
            return context.catalogs[id];
        }
        public Catalog GetRecentCatalog()
        {
            return context.catalogs[counter];
        }
        public List<Catalog> GetAllCatalogs()
        {
            return context.catalogs.Values.ToList();
        }

        public void RemoveCatalog(int id)
        {
            foreach(var eve in context.events) {
                //here create something that checks if a catalog object is used by an event, if so, throw exception
            }
            context.catalogs.Remove(id);
        }
        #endregion

        #region Events

        public void AddEvent(Event eve)
        {
            context.events.Add(eve);
        }
        public Event GetEvent(int id)
        {
            return context.events[id];
        }
        public List<Event> GetAllEvents()
        {
            return context.events.ToList();
        }

        public void RemoveEvent(int id) 
        {
            if (id >= context.events.Count())
                throw new Exception("Event does not exist");
            context.events.Remove(context.events[id]);
        }
        #endregion

        #region State
        public void AddState(State state)
        {
            context.rooms.Add(state);
        }
        public State GetState(int id) { return context.rooms[id]; }
        public List<State> GetAllStates()
        {
            return context.rooms;
        }
        public void RemoveState(int id)
        {
            //check if state used, if so, throw an exception
            context.rooms.Remove(GetState(id));
        }
        #endregion
    }

}
