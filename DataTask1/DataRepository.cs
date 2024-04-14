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

        #region Associations with events
        // Method to check if a user is associated with an event
        public bool IsUserInEvent(User user)
        {
            foreach (var evt in context.events)
            {
                if (evt.guest == user)
                {
                    return true;
                }
            }
            return false;
        }

        // Method to check if a room is associated with an event
        public bool IsRoomInEvent(State room)
        {
            foreach (var evt in context.events)
            {
                if (evt.room == room)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Users
        public void AddUser(User user)
        {
            context.users.Add(user);
        }
        public User GetUser(int id)
        {
            foreach (var user in context.users)
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
            if (IsUserInEvent(GetUser(id)) == true)
            {
                throw new Exception("User has an event assigned. Cannot remove user.");
            }
            else
            {
                foreach (var user in context.users)
                {
                    if (user.Id == id)
                    {
                        context.users.Remove(user);
                    }
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
            foreach (var eve in context.events)
            {
                //here create something that checks if a catalog object is used by an event, if so, throw exception
            }
            context.catalogs.Remove(id);
        }
        #endregion

        #region Events

        public void AddEvent(Event eve)
        {
            context.events.Add(eve);
            eve.Room.RoomCatalog.SetIsBooked(true);
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
            Event eve = context.events[id];

            if (id >= context.events.Count())
            {
                throw new Exception("Event does not exist");
            }

            else
            {
                eve.Room.RoomCatalog.SetIsBooked(false);
                context.events.Remove(eve);
            }
        }
        #endregion

        #region State
        public void AddState(State state)
        {
            context.rooms.Add(state);
        }
        public State GetState(int id)
        {
            return context.rooms[id];
        }
        public List<State> GetAllStates()
        {
            return context.rooms;
        }
        public void RemoveState(int id)
        {
            if (IsRoomInEvent(GetState(id)) == true) throw new Exception("State used by an event. Cannot remove this state.");
                context.rooms.Remove(GetState(id));
        }
        #endregion

        }

}
