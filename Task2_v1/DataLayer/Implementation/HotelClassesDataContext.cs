using DataLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public partial class HotelClassesDataContext : IDataContext
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\HotelDatabase.mdf;Integrated Security=False";
        #region User CRUD

        public async Task AddUserAsync(IUser user)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.User entity = new Database.User()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                };

                context.Users.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IUser> GetUserAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.User user = await Task.Run(() =>
                {
                    IQueryable<Database.User> query = context.Users.Where(u => u.Id == Id);

                    return query.FirstOrDefault();
                });

                if (user is null)
                {
                    return null;
                }
                else
                {
                    return new Implementation.User(user.Id, user.FirstName, user.LastName, user.UserType);
                }
            }
        }

        public async Task UpdateUserAsync(IUser user)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.User toUpdate = (from u in context.Users where u.Id == user.Id select u).FirstOrDefault();

                toUpdate.FirstName = user.FirstName;
                toUpdate.LastName = user.LastName;
                toUpdate.UserType = user.UserType;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteUserAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.User toDelete = (from u in context.Users where u.Id == Id select u).FirstOrDefault();

                context.Users.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<int, IUser>> GetAllUsersAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                IQueryable<IUser> usersQuery = from u in context.Users
                                               select
                                                   new Implementation.User(u.Id, u.FirstName, u.LastName, u.UserType) as IUser;

                return await Task.Run(() => usersQuery.ToDictionary(k => k.Id));
            }
        }

        public async Task<int> GetUsersCountAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.Users.Count());
            }
        }

        #endregion


        #region Catalog CRUD

        public async Task AddCatalogAsync(ICatalog Catalog)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Catalog entity = new Database.Catalog()
                {
                    Id = Catalog.Id,
                    RoomNumber = Catalog.RoomNumber,
                    RoomType = Catalog.RoomType,
                    IsBooked = Catalog.isBooked
                };

                context.Catalogs.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<ICatalog> GetCatalogAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Catalog Catalog = await Task.Run(() =>
                {
                    IQueryable<Database.Catalog> query =
                        from p in context.Catalogs
                        where p.Id == Id
                        select p;

                    return query.FirstOrDefault();
                });

                if (Catalog is null)
                {
                    return null;
                }
                else
                {
                    return new Implementation.Catalog(Catalog.Id, Catalog.RoomNumber, Catalog.RoomType, Catalog.IsBooked);
                }
            }
        }

        public async Task UpdateCatalogAsync(ICatalog Catalog)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Catalog toUpdate = (from p in context.Catalogs where p.Id == Catalog.Id select p).FirstOrDefault();

                toUpdate.RoomNumber = Catalog.RoomNumber;
                toUpdate.RoomType = Catalog.RoomType;
                toUpdate.IsBooked = Catalog.isBooked;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteCatalogAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Catalog toDelete = (from p in context.Catalogs where p.Id == Id select p).FirstOrDefault();

                context.Catalogs.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<int, ICatalog>> GetAllCatalogsAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                IQueryable<ICatalog> CatalogQuery = from p in context.Catalogs
                                                    select
                                                        new Implementation.Catalog(p.Id, p.RoomNumber, p.RoomType, p.IsBooked) as ICatalog;

                return await Task.Run(() => CatalogQuery.ToDictionary(k => k.Id));
            }
        }

        public async Task<int> GetCatalogsCountAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.Catalogs.Count());
            }
        }

        #endregion


        #region State CRUD

        public async Task AddStateAsync(IState state)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.State entity = new Database.State()
                {
                    Id = state.Id,
                    RoomCatalogId = state.RoomCatalogId,
                    Price = state.Price
                };

                context.States.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IState> GetStateAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.State state = await Task.Run(() =>
                {
                    IQueryable<Database.State> query =
                        from s in context.States
                        where s.Id == Id
                        select s;

                    return query.FirstOrDefault();
                });

                if (state is null)
                {
                    return null;
                }
                else
                {
                    return new Implementation.State(state.Id, state.RoomCatalogId, state.Price);
                }
            }
        }

        public async Task UpdateStateAsync(IState state)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.State toUpdate = (from s in context.States where s.Id == state.Id select s).FirstOrDefault();

                toUpdate.RoomCatalogId = state.RoomCatalogId;
                toUpdate.Price = state.Price;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteStateAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.State toDelete = (from s in context.States where s.Id == Id select s).FirstOrDefault();

                context.States.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<int, IState>> GetAllStatesAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                IQueryable<IState> stateQuery = from s in context.States
                                                select
                                                    new Implementation.State(s.Id, s.RoomCatalogId, s.Price) as IState;

                return await Task.Run(() => stateQuery.ToDictionary(k => k.Id));
            }
        }

        public async Task<int> GetStatesCountAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.States.Count());
            }
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(IEvent even)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Event entity = new Database.Event()
                {
                    Id = even.Id,
                    StateId = even.StateId,
                    UserId = even.UserId,
                    CheckInDate = even.CheckInDate,
                    CheckOutDate = even.CheckOutDate,                    
                };

                context.Events.InsertOnSubmit(entity);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<IEvent> GetEventAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Event even = await Task.Run(() =>
                {
                    IQueryable<Database.Event> query =
                        from e in context.Events
                        where e.Id == Id
                        select e;

                    return query.FirstOrDefault();
                });

                if (even is null)
                {
                    return null;
                }
                else
                {
                    return new Implementation.Event(even.Id, even.StateId, even.UserId, even.CheckInDate, even.CheckOutDate);
                }
            }

        }

        public async Task UpdateEventAsync(IEvent even)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Event toUpdate = (from e in context.Events where e.Id == even.Id select e).FirstOrDefault();

                toUpdate.StateId = even.StateId;
                toUpdate.UserId = even.UserId;
                toUpdate.CheckInDate = even.CheckInDate;
                toUpdate.CheckOutDate = even.CheckOutDate;

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteEventAsync(int Id)
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                Database.Event toDelete = (from e in context.Events where e.Id == Id select e).FirstOrDefault();

                context.Events.DeleteOnSubmit(toDelete);

                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task<Dictionary<int, IEvent>> GetAllEventsAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                IQueryable<IEvent> eventQuery = from e in context.Events
                                                select
                                                    new Implementation.Event(e.Id, e.StateId, e.UserId, e.CheckInDate, e.CheckOutDate) as IEvent;

                return await Task.Run(() => eventQuery.ToDictionary(k => k.Id));
            }
        }

        public async Task<int> GetEventsCountAsync()
        {
            using (var context= new HotelClassesDataContext(_connectionString))
            {
                return await Task.Run(() => context.Events.Count());
            }
        }
        public Task<IEnumerable<IEvent>> GetEventsForUser(int userId)
        {
            return Task.Run(() =>
            {
                using (var context = new HotelClassesDataContext(_connectionString))
                {
                    return context.Events.Where(e => e.UserId == userId).ToList() as IEnumerable<IEvent>;
                }
            });
        }
        #endregion


        #region CheckIfExists

        public async Task<bool> CheckIfUserExists(int Id)
        {
            return (await GetUserAsync(Id)) != null;
        }

        public async Task<bool> CheckIfCatalogExists(int Id)
        {
            return (await GetCatalogAsync(Id)) != null;
        }

        public async Task<bool> CheckIfStateExists(int Id)
        {
            return (await GetStateAsync(Id)) != null;
        }

        public async Task<bool> CheckIfEventExists(int Id, string type)
        {
            return (await GetEventAsync(Id)) != null;
        }

        #endregion

    }
}
