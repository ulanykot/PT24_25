using DataLayer.API;
using DataLayer.Database;
using DataLayer.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataLayerTest
{
    [TestClass]
    public class DataTest
    {
        private string _connectionString;

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Task2_v1.Properties.Settings.TestHotelConnectionString"].ConnectionString;
        }

        #region User Tests
        [TestMethod]
        public async Task AddUserAsync_ShouldAddUserToDatabase()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int userId = 7;
            string firstName = "Jane";
            string lastName = "Doe";
            string userType = "Guest";

            await dataRepository.AddUserAsync(userId, firstName, lastName, userType);

            var addedUser = await dataRepository.GetUserAsync(userId);
            Assert.IsNotNull(addedUser, "User should be added to the database");
            Assert.AreEqual(userId, addedUser.Id, "User ID should match the provided ID");
            Assert.AreEqual(firstName, addedUser.FirstName, "User first name should match the provided first name");
            Assert.AreEqual(lastName, addedUser.LastName, "User last name should match the provided last name");
            Assert.AreEqual(userType, addedUser.UserType, "User type should match the provided user type");
        }

        [TestMethod]
        public async Task GetUserAsync_ShouldReturnUserIfExists()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int userId = 8;
            string firstName = "James";
            string lastName = "Done";
            string userType = "Staff";
            await dataRepository.AddUserAsync(userId, firstName, lastName, userType);

            var retrievedUser = await dataRepository.GetUserAsync(userId);

            Assert.IsNotNull(retrievedUser, "Retrieved user should not be null");
            Assert.AreEqual(userId, retrievedUser.Id, "Retrieved user ID should match the provided ID");
            Assert.AreEqual(firstName, retrievedUser.FirstName, "Retrieved user first name should match the provided first name");
            Assert.AreEqual(lastName, retrievedUser.LastName, "Retrieved user last name should match the provided last name");
            Assert.AreEqual(userType, retrievedUser.UserType, "Retrieved user type should match the provided user type");
        }

        [TestMethod]
        public async Task UpdateUserAsync_ShouldUpdateExistingUser()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int userId = 9;
            string firstName = "John";
            string lastName = "Doe";
            string userType = "Guest";
            await dataRepository.AddUserAsync(userId, firstName, lastName, userType);

            string newFirstName = "Jane";
            string newLastName = "Smith";
            string newUserType = "Admin";

            await dataRepository.UpdateUserAsync(userId, newFirstName, newLastName, newUserType);

            var updatedUser = await dataRepository.GetUserAsync(userId);

            Assert.IsNotNull(updatedUser, "Updated user should not be null");
            Assert.AreEqual(userId, updatedUser.Id, "User ID should remain unchanged after update");
            Assert.AreEqual(newFirstName, updatedUser.FirstName, "User first name should be updated");
            Assert.AreEqual(newLastName, updatedUser.LastName, "User last name should be updated");
            Assert.AreEqual(newUserType, updatedUser.UserType, "User type should be updated");
        }

        [TestMethod]
        public async Task DeleteUserAsync_ShouldDeleteExistingUser()
        {
            
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int userId = 10;
            string firstName = "Jane";
            string lastName = "Doe";
            string userType = "Guest";
            await dataRepository.AddUserAsync(userId, firstName, lastName, userType);

            await dataRepository.DeleteUserAsync(userId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await dataRepository.GetUserAsync(userId), "This user does not exist!");
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);

            var users = new List<(int Id, string FirstName, string LastName, string UserType)>
            {
                (11, "John", "Doe", "Regular"),
                (12, "Jane", "Smith", "Premium")
            };

            foreach (var user in users)
            {
                await dataRepository.AddUserAsync(user.Id, user.FirstName, user.LastName, user.UserType);
            }

            var allUsers = await dataRepository.GetAllUsersAsync();

            foreach (var user in users)
            {
                Assert.IsTrue(allUsers.ContainsKey(user.Id), $"User with ID {user.Id} should be in the retrieved list");
                var retrievedUser = allUsers[user.Id];
                Assert.AreEqual(user.FirstName, retrievedUser.FirstName, $"User first name should be {user.FirstName}");
                Assert.AreEqual(user.LastName, retrievedUser.LastName, $"User last name should be {user.LastName}");
                Assert.AreEqual(user.UserType, retrievedUser.UserType, $"User type should be {user.UserType}");
            }
        }
        #endregion

        #region Catalog Tests
        [TestMethod]
        public async Task AddCatalogAsync_ShouldAddCatalogToDatabase()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 10;
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            var addedCatalog = await dataRepository.GetCatalogAsync(catalogId);
            Assert.IsNotNull(addedCatalog, "Catalog should be added to the database");
            Assert.AreEqual(catalogId, addedCatalog.Id, "Catalog ID should match the provided ID");
            Assert.AreEqual(roomNumber, addedCatalog.RoomNumber, "Room number should match the provided room number");
            Assert.AreEqual(roomType, addedCatalog.RoomType, "Room type should match the provided room type");
            Assert.AreEqual(isBooked, addedCatalog.isBooked, "Booking status should match the provided status");
        }

        [TestMethod]
        public async Task GetCatalogAsync_ShouldReturnCatalogIfExists()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 5;
            int roomNumber = 102;
            string roomType = "Double";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            var retrievedCatalog = await dataRepository.GetCatalogAsync(catalogId);

            Assert.IsNotNull(retrievedCatalog, "Retrieved catalog should not be null");
            Assert.AreEqual(catalogId, retrievedCatalog.Id, "Catalog ID should match the provided ID");
            Assert.AreEqual(roomNumber, retrievedCatalog.RoomNumber, "Room number should match the provided room number");
            Assert.AreEqual(roomType, retrievedCatalog.RoomType, "Room type should match the provided room type");
            Assert.AreEqual(isBooked, retrievedCatalog.isBooked, "Booking status should match the provided status");
        }

        [TestMethod]
        public async Task UpdateCatalogAsync_ShouldUpdateExistingCatalog()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 6;
            int roomNumber = 103;
            string roomType = "Single";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int newRoomNumber = 104;
            string newRoomType = "Suite";
            bool newIsBooked = true;

            await dataRepository.UpdateCatalogAsync(catalogId, newRoomNumber, newRoomType, newIsBooked);

            var updatedCatalog = await dataRepository.GetCatalogAsync(catalogId);

            Assert.IsNotNull(updatedCatalog, "Updated catalog should not be null");
            Assert.AreEqual(catalogId, updatedCatalog.Id, "Catalog ID should remain unchanged after update");
            Assert.AreEqual(newRoomNumber, updatedCatalog.RoomNumber, "Room number should be updated");
            Assert.AreEqual(newRoomType, updatedCatalog.RoomType, "Room type should be updated");
            Assert.AreEqual(newIsBooked, updatedCatalog.isBooked, "Booking status should be updated");
        }

        [TestMethod]
        public async Task DeleteCatalogAsync_ShouldDeleteExistingCatalog()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 7;
            int roomNumber = 105;
            string roomType = "Double";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            await dataRepository.DeleteCatalogAsync(catalogId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await dataRepository.GetCatalogAsync(catalogId), "This catalog does not exist!");
        }

        [TestMethod]
        public async Task GetAllCatalogsAsync_ShouldReturnAllCatalogs()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);

            var catalogs = new List<(int Id, int RoomNumber, string RoomType, bool IsBooked)>
            {
                (8, 106, "Single", false),
                (9, 107, "Suite", true)
            };

            foreach (var catalog in catalogs)
            {
                await dataRepository.AddCatalogAsync(catalog.Id, catalog.RoomNumber, catalog.RoomType, catalog.IsBooked);
            }

            var allCatalogs = await dataRepository.GetAllCatalogsAsync();

            foreach (var catalog in catalogs)
            {
                Assert.IsTrue(allCatalogs.ContainsKey(catalog.Id), $"Catalog with ID {catalog.Id} should be in the retrieved list");
                var retrievedCatalog = allCatalogs[catalog.Id];
                Assert.AreEqual(catalog.RoomNumber, retrievedCatalog.RoomNumber, $"Room number should be {catalog.RoomNumber}");
                Assert.AreEqual(catalog.RoomType, retrievedCatalog.RoomType, $"Room type should be {catalog.RoomType}");
                Assert.AreEqual(catalog.IsBooked, retrievedCatalog.isBooked, $"Booking status should be {catalog.IsBooked}");
            }
        }

        #endregion

        #region State Tests
        [TestMethod]
        public async Task AddStateAsync_ShouldAddStateToDatabase()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 20;
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int stateId = 20;
            int roomId = catalogId;
            int price = 100;

            await dataRepository.AddStateAsync(stateId, roomId, price);

            var addedState = await dataRepository.GetStateAsync(stateId);
            Assert.IsNotNull(addedState, "State should be added to the database");
            Assert.AreEqual(stateId, addedState.Id, "State ID should match the provided ID");
            Assert.AreEqual(roomId, addedState.RoomCatalogId, "Room ID should match the provided room ID");
            Assert.AreEqual(price, addedState.Price, "Price should match the provided price");
        }

        [TestMethod]
        public async Task GetStateAsync_ShouldReturnStateIfExists()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 21;
            int roomNumber = 102;
            string roomType = "Double";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int stateId = 21;
            int roomId = catalogId;
            int price = 200;

            await dataRepository.AddStateAsync(stateId, roomId, price);

            var retrievedState = await dataRepository.GetStateAsync(stateId);

            Assert.IsNotNull(retrievedState, "Retrieved state should not be null");
            Assert.AreEqual(stateId, retrievedState.Id, "State ID should match the provided ID");
            Assert.AreEqual(roomId, retrievedState.RoomCatalogId, "Room ID should match the provided room ID");
            Assert.AreEqual(price, retrievedState.Price, "Price should match the provided price");
        }

        [TestMethod]
        public async Task UpdateStateAsync_ShouldUpdateExistingState()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 22;
            int roomNumber = 103;
            string roomType = "Single";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int stateId = 22;
            int roomId = catalogId;
            int price = 300;

            await dataRepository.AddStateAsync(stateId, roomId, price);

            int newPrice = 350;

            await dataRepository.UpdateStateAsync(stateId, roomId, newPrice);

            var updatedState = await dataRepository.GetStateAsync(stateId);

            Assert.IsNotNull(updatedState, "Updated state should not be null");
            Assert.AreEqual(stateId, updatedState.Id, "State ID should remain unchanged after update");
            Assert.AreEqual(roomId, updatedState.RoomCatalogId, "Room ID should remain unchanged after update");
            Assert.AreEqual(newPrice, updatedState.Price, "Price should be updated");
        }

        [TestMethod]
        public async Task DeleteStateAsync_ShouldDeleteExistingState()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int catalogId = 23;
            int roomNumber = 104;
            string roomType = "Double";
            bool isBooked = false;

            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int stateId = 23;
            int roomId = catalogId;
            int price = 400;

            await dataRepository.AddStateAsync(stateId, roomId, price);

            await dataRepository.DeleteStateAsync(stateId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await dataRepository.GetStateAsync(stateId), "This state does not exist!");
        }

        [TestMethod]
        public async Task GetAllStatesAsync_ShouldReturnAllStates()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);

            int catalogId1 = 25;
            int roomNumber1 = 105;
            string roomType1 = "Single";
            bool isBooked1 = false;
            await dataRepository.AddCatalogAsync(catalogId1, roomNumber1, roomType1, isBooked1);

            int catalogId2 = 26;
            int roomNumber2 = 106;
            string roomType2 = "Suite";
            bool isBooked2 = true;
            await dataRepository.AddCatalogAsync(catalogId2, roomNumber2, roomType2, isBooked2);

            var states = new List<(int Id, int RoomCatalogId, int Price)>
            {
                (25, catalogId1, 100),
                (26, catalogId2, 200)
            };

            foreach (var state in states)
            {
                await dataRepository.AddStateAsync(state.Id, state.RoomCatalogId, state.Price);
            }

            var allStates = await dataRepository.GetAllStatesAsync();

            foreach (var state in states)
            {
                Assert.IsTrue(allStates.ContainsKey(state.Id), $"State with ID {state.Id} should be in the retrieved list");
                var retrievedState = allStates[state.Id];
                Assert.AreEqual(state.RoomCatalogId, retrievedState.RoomCatalogId, $"Room catalog ID should be {state.RoomCatalogId}");
                Assert.AreEqual(state.Price, retrievedState.Price, $"Price should be {state.Price}");
            }
        }

        #endregion

        #region Event Tests

        [TestMethod]
        public async Task AddEventAsync_ShouldAddEventToDatabase()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int eventId = 7;
            int stateId = 3;
            int userId = 1;
            DateTime checkIn = DateTime.Now;
            DateTime checkOut = DateTime.Now.AddDays(2);
            string type = "CheckIn";

            await dataRepository.AddEventAsync(eventId, stateId, userId, checkIn, checkOut, type);

            var addedEvent = await dataRepository.GetEventAsync(eventId);
            Assert.IsNotNull(addedEvent, "Event should be added to the database");
            Assert.AreEqual(eventId, addedEvent.Id, "Event ID should match the provided ID");
            Assert.AreEqual(stateId, addedEvent.StateId, "State ID should match the provided state ID");
            Assert.AreEqual(userId, addedEvent.UserId, "User ID should match the provided user ID");
            Assert.IsTrue(DateTime.Equals(checkIn.Date, addedEvent.CheckInDate?.Date), "Check-in date should match the provided date");
            Assert.IsTrue(DateTime.Equals(checkOut.Date, addedEvent.CheckOutDate?.Date), "Check-out date should match the provided date");
        }

        [TestMethod]
        public async Task GetEventAsync_ShouldReturnEventIfExists()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);

            int catalogId = 43;
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;
            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int stateId = 43;
            int roomId = catalogId;
            int price = 100;
            await dataRepository.AddStateAsync(stateId, roomId, price);

            int eventId = 8;
            int userId = 1;
            DateTime checkIn = DateTime.Now;
            DateTime checkOut = DateTime.Now.AddDays(2);
            string type = "CheckIn";

            await dataRepository.AddEventAsync(eventId, stateId, userId, checkIn, checkOut, type);

            var retrievedEvent = await dataRepository.GetEventAsync(eventId);

            Assert.IsNotNull(retrievedEvent, "Retrieved event should not be null");
            Assert.AreEqual(eventId, retrievedEvent.Id, "Retrieved event ID should match the provided ID");
            Assert.AreEqual(stateId, retrievedEvent.StateId, "Retrieved event state ID should match the provided state ID");
            Assert.AreEqual(userId, retrievedEvent.UserId, "Retrieved event user ID should match the provided user ID");
            Assert.IsTrue(DateTime.Equals(checkIn.Date, retrievedEvent.CheckInDate?.Date), "Check-in date should match the provided date");
            Assert.IsTrue(DateTime.Equals(checkOut.Date, retrievedEvent.CheckOutDate?.Date), "Check-out date should match the provided date");
        }

        /*[TestMethod]*/
        /*public async Task UpdateEventAsync_ShouldUpdateExistingEvent()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int eventId = 9;
            int stateId = 3;
            int userId = 1;
            DateTime checkIn = DateTime.Now;
            DateTime checkOut = DateTime.Now.AddDays(2);
            string type = "CheckIn";
            await dataRepository.AddEventAsync(eventId, stateId, userId, checkIn, checkOut, type);

            int newStateId = 2;
            int newUserId = 2;
            DateTime newCheckIn = DateTime.Now.AddDays(5);
            DateTime newCheckOut = DateTime.Now.AddDays(7);
            string newType = "CheckOut";
            await dataRepository.UpdateEventAsync(eventId, newStateId, newUserId, newCheckIn, newCheckOut, newType);

            var updatedEvent = await dataRepository.GetEventAsync(eventId);

            Assert.IsNotNull(updatedEvent, "Updated event should not be null");
            Assert.AreEqual(eventId, updatedEvent.Id, "Event ID should remain unchanged after update");
            //Assert.AreEqual(newStateId, updatedEvent.StateId, "Event state ID should be updated");
            Assert.AreEqual(newUserId, updatedEvent.UserId, "Event user ID should be updated");
            Assert.IsTrue(updatedEvent.CheckInDate == newCheckIn, "Event check-in date should be updated");
            Assert.IsTrue(updatedEvent.CheckOutDate == newCheckOut, "Event check-out date should be updated");
        }*/

        [TestMethod]
        public async Task DeleteEventAsync_ShouldDeleteExistingEvent()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            int eventId = 10;
            int stateId = 1;
            int userId = 1;
            DateTime checkIn = DateTime.Now;
            DateTime checkOut = DateTime.Now.AddDays(2);
            string type = "CheckIn";
            await dataRepository.AddEventAsync(eventId, stateId, userId, checkIn, checkOut, type);

            await dataRepository.DeleteEventAsync(eventId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await dataRepository.GetEventAsync(eventId), "This event does not exist!");
        }

        [TestMethod]
        public async Task GetAllEventsAsync_ShouldReturnAllEvents()
        {
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            var dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);

            int catalogId = 42;
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;
            await dataRepository.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int stateId = 42;
            int roomId = catalogId;
            int price = 100;
            await dataRepository.AddStateAsync(stateId, roomId, price);

            var events = new List<(int Id, int StateId, int UserId, DateTime CheckIn, DateTime CheckOut, string Type)>
            {
                (11, stateId, 1, DateTime.Now, DateTime.Now.AddDays(2), "CheckIn"),
                (12, stateId, 2, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3), "CheckOut")
            };

            foreach (var e in events)
            {
                await dataRepository.AddEventAsync(e.Id, e.StateId, e.UserId, e.CheckIn, e.CheckOut, e.Type);
            }

            var allEvents = await dataRepository.GetAllEventsAsync();

            foreach (var e in events)
            {
                Assert.IsTrue(allEvents.ContainsKey(e.Id), $"Event with ID {e.Id} should be in the retrieved list");
                var retrievedEvent = allEvents[e.Id];
                Assert.AreEqual(e.UserId, retrievedEvent.UserId, $"Event user ID should be {e.UserId}");
                Assert.IsTrue(DateTime.Equals(e.CheckIn.Date, retrievedEvent.CheckInDate?.Date), "Check-in date should match the provided date");
                Assert.IsTrue(DateTime.Equals(e.CheckOut.Date, retrievedEvent.CheckOutDate?.Date), "Check-out date should match the provided date");
            }
        }

        #endregion
    }
}
