using DataLayer.API;
using DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation
{
    internal class DataRepository : IDataRepository
    {
        private IDataContext _context;
        public DataRepository(IDataContext context)
        {
            _context = context;
        }

        #region User CRUD

        public async Task AddUserAsync(int id, string firstName, string lastName, string userType) 
        {
            IUser user = new User(id, firstName, lastName, userType);
            await _context.AddUserAsync(user);
        }

        public async Task<IUser> GetUserAsync(int id)
        {
            IUser user = await _context.GetUserAsync(id);

            return user is null ? throw new Exception("This user does not exist!") : user;
        }

        public async Task UpdateUserAsync(int id, string firstName, string lastName, string userType)
        {
            IUser user = new User(id, firstName, lastName, userType);
            if (!await _context.CheckIfUserExists(user.Id))
                throw new Exception("This user does not exist");

            await _context.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            if (!await _context.CheckIfUserExists(id))
                throw new Exception("This user does not exist");

            await _context.DeleteUserAsync(id);
        }

        public async Task<Dictionary<int, IUser>> GetAllUsersAsync()
        {
            return await _context.GetAllUsersAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _context.GetUsersCountAsync();
        }

        #endregion User CRUD


        #region Catalog CRUD

        public async Task AddCatalogAsync(int id, int roomNumber, string roomType, bool isBooked)
        {
            ICatalog catalog = new Catalog(id,roomNumber,roomType,isBooked);
            await _context.AddCatalogAsync(catalog);
        }

        public async Task<ICatalog> GetCatalogAsync(int id)
        {
            ICatalog catalog = await _context.GetCatalogAsync(id);
            if (catalog is null)
                throw new Exception("This catalog does not exist!");
            return catalog;
        }

        public async Task UpdateCatalogAsync(int id, int? roomNumber, string roomType, bool? isBooked)
        {
            ICatalog catalog = new Catalog(id, roomNumber,roomType,isBooked);

            if (!await _context.CheckIfCatalogExists(catalog.Id))
                throw new Exception("This catalog does not exist!");

            await _context.UpdateCatalogAsync(catalog);
        }

        public async Task DeleteCatalogAsync(int id)
        {
            if (!await _context.CheckIfCatalogExists(id))
                throw new Exception("This catalog does not exist!");

            await _context.DeleteCatalogAsync(id);
        }

        public async Task<Dictionary<int, ICatalog>> GetAllCatalogsAsync()
        {
            return await _context.GetAllCatalogsAsync();
        }

        public async Task<int> GetCatalogsCountAsync()
        {
            return await _context.GetCatalogsCountAsync();
        }

        #endregion


        #region State CRUD

        public async Task AddStateAsync(int id, int roomId, int price)
        {
            if (!await _context.CheckIfCatalogExists(roomId))
                throw new Exception("This catalog does not exist!");

            IState state = new State(id, roomId, price);

            await _context.AddStateAsync(state);
        }

        public async Task<IState> GetStateAsync(int id)
        {
            IState state = await _context.GetStateAsync(id);

            if (state is null)
                throw new Exception("This state does not exist!");

            return state;
        }

        public async Task UpdateStateAsync(int id, int roomId, int price)
        {
            if (!await _context.CheckIfCatalogExists(roomId))
                throw new Exception("This catalog does not exist!");

            IState state = new State(id, roomId, price);
            
            if(!await _context.CheckIfStateExists(state.Id))
            {
                throw new Exception("This state does not exist");
            }
            await _context.UpdateStateAsync(state);
        }

        public async Task DeleteStateAsync(int id)
        {
            if (!await _context.CheckIfStateExists(id))
                throw new Exception("This state does not exist");

            await _context.DeleteStateAsync(id);
        }

        public async Task<Dictionary<int, IState>> GetAllStatesAsync()
        {
            return await _context.GetAllStatesAsync();
        }

        public async Task<int> GetStatesCountAsync()
        {
            return await _context.GetStatesCountAsync();
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
        {
            IUser user = await GetUserAsync(userId);
            IState state = await GetStateAsync(stateId);
            ICatalog catalog = await GetCatalogAsync((int)state.RoomCatalogId);
            IEvent newEvent = new Event(id, stateId, userId, checkInDate, checkOutDate, type);
            
            switch (type)
            {
                case "CheckIn":
                    if (catalog.isBooked == true)
                        throw new Exception("Room unavailable, please check later!");

                    await UpdateCatalogAsync((int)state.RoomCatalogId, catalog.RoomNumber, catalog.RoomType, catalog.isBooked = true);
                    await UpdateUserAsync(userId, user.FirstName, user.LastName, user.UserType);

                    break;

                case "CheckOut":
                    if (catalog.isBooked == false)
                        throw new Exception("Room is not even booked!");

                    await UpdateCatalogAsync((int)state.RoomCatalogId, catalog.RoomNumber, catalog.RoomType, false);
                    await UpdateUserAsync(userId, user.FirstName, user.LastName, user.UserType);

                    break;

               
                default:
                    throw new Exception("This event type does not exist!");
            }

            await _context.AddEventAsync(newEvent);
        }

        public async Task<IEvent> GetEventAsync(int id)
        {
            IEvent even = await _context.GetEventAsync(id);

            if (even is null)
                throw new Exception("This event does not exist!");

            return even;
        }

        public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime checkInDate, DateTime checkOutDate, string type)
        {
            IEvent newEvent = new Event(id, stateId, userId, checkInDate, checkOutDate, type);
            if (!await _context.CheckIfEventExists(newEvent.Id, type))
                throw new Exception("This event does not exist");

            await _context.UpdateEventAsync(newEvent);
        }

        public async Task DeleteEventAsync(int id)
        {
            if ((!await _context.CheckIfEventExists(id, "CheckOut")|| !await _context.CheckIfEventExists(id,"CheckIn")))
                throw new Exception("This event does not exist");

            await _context.DeleteEventAsync(id);
        }

        public async Task<Dictionary<int, IEvent>> GetAllEventsAsync()
        {
            return await _context.GetAllEventsAsync();
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await _context.GetEventsCountAsync();
        }
        public async Task<IEnumerable<IEvent>> GetEventsForUser(int userId)
        {
            return await _context.GetEventsForUser(userId);
        }

        #endregion
    }
}
