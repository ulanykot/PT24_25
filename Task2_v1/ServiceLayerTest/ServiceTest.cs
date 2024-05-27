using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer.API;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using DataLayer.API;
using DataLayer.Database;
using DataLayer.Implementation;

namespace ServiceLayerTest
{
    [TestClass]
    public class UserServiceTests
    {
        private string _connectionString;
        private IDataRepository _dataRepository;
        private IUserService _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Task2_v1.Properties.Settings.TestHotelConnectionString"].ConnectionString;
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            _dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            _userService = ServiceFactory.CreateUserService(_dataRepository); 
        }

        [TestMethod]
        public async Task AddUserAsync_ShouldAddUserToDatabase()
        {
            int userId = 4;
            string firstName = "John";
            string lastName = "Doe";
            string userType = "Guest";

            await _userService.AddUserAsync(userId, firstName, lastName, userType);

            var addedUser = await _userService.GetUserAsync(userId);
            Assert.IsNotNull(addedUser, "User should be added to the database");
            Assert.AreEqual(userId, addedUser.Id, "User ID should match the provided ID");
            Assert.AreEqual(firstName, addedUser.FirstName, "User first name should match the provided first name");
            Assert.AreEqual(lastName, addedUser.LastName, "User last name should match the provided last name");
            Assert.AreEqual(userType, addedUser.UserType, "User type should match the provided user type");
        }

        [TestMethod]
        public async Task GetUserAsync_ShouldReturnUserIfExists()
        {
            int userId = 8;
            string firstName = "Jane";
            string lastName = "Smith";
            string userType = "Admin";

            await _userService.AddUserAsync(userId, firstName, lastName, userType);

            var retrievedUser = await _userService.GetUserAsync(userId);

            Assert.IsNotNull(retrievedUser, "Retrieved user should not be null");
            Assert.AreEqual(userId, retrievedUser.Id, "Retrieved user ID should match the provided ID");
            Assert.AreEqual(firstName, retrievedUser.FirstName, "Retrieved user first name should match the provided first name");
            Assert.AreEqual(lastName, retrievedUser.LastName, "Retrieved user last name should match the provided last name");
            Assert.AreEqual(userType, retrievedUser.UserType, "Retrieved user type should match the provided user type");
        }

        [TestMethod]
        public async Task UpdateUserAsync_ShouldUpdateExistingUser()
        {
            int userId = 9;
            string firstName = "Alice";
            string lastName = "Johnson";
            string userType = "Guest";
            await _userService.AddUserAsync(userId, firstName, lastName, userType);

            string newFirstName = "Alicia";
            string newLastName = "John";
            string newUserType = "VIP";

            await _userService.UpdateUserAsync(userId, newFirstName, newLastName, newUserType);

            var updatedUser = await _userService.GetUserAsync(userId);

            Assert.IsNotNull(updatedUser, "Updated user should not be null");
            Assert.AreEqual(userId, updatedUser.Id, "User ID should remain unchanged after update");
            Assert.AreEqual(newFirstName, updatedUser.FirstName, "User first name should be updated");
            Assert.AreEqual(newLastName, updatedUser.LastName, "User last name should be updated");
            Assert.AreEqual(newUserType, updatedUser.UserType, "User type should be updated");
        }

        [TestMethod]
        public async Task DeleteUserAsync_ShouldDeleteExistingUser()
        {
            int userId = 7;
            string firstName = "Bob";
            string lastName = "Brown";
            string userType = "Guest";
            await _userService.AddUserAsync(userId, firstName, lastName, userType);

            await _userService.DeleteUserAsync(userId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _userService.GetUserAsync(userId), "This user does not exist!");
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            var users = new List<(int Id, string FirstName, string LastName, string UserType)>
            {
                (5, "Charlie", "Day", "Regular"),
                (6, "Dee", "Reynolds", "Premium")
            };

            foreach (var user in users)
            {
                await _userService.AddUserAsync(user.Id, user.FirstName, user.LastName, user.UserType);
            }

            var allUsers = await _userService.GetAllUsersAsync();

            foreach (var user in users)
            {
                Assert.IsTrue(allUsers.ContainsKey(user.Id), $"User with ID {user.Id} should be in the retrieved list");
                var retrievedUser = allUsers[user.Id];
                Assert.AreEqual(user.FirstName, retrievedUser.FirstName, $"User first name should be {user.FirstName}");
                Assert.AreEqual(user.LastName, retrievedUser.LastName, $"User last name should be {user.LastName}");
                Assert.AreEqual(user.UserType, retrievedUser.UserType, $"User type should be {user.UserType}");
            }
        }
    }

    [TestClass]
    public class CatalogServiceTests
    {
        private string _connectionString;
        private IDataRepository _dataRepository;
        private ICatalogService _catalogService;

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Task2_v1.Properties.Settings.TestHotelConnectionString"].ConnectionString;
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            _dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            _catalogService = ServiceFactory.CreateCatalogService(_dataRepository);
        }

        [TestMethod]
        public async Task AddCatalogAsync_ShouldAddCatalogToDatabase()
        {
            int catalogId = 7;
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;

            await _catalogService.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            var addedCatalog = await _catalogService.GetCatalogAsync(catalogId);
            Assert.IsNotNull(addedCatalog, "Catalog should be added to the database");
            Assert.AreEqual(catalogId, addedCatalog.Id, "Catalog ID should match the provided ID");
            Assert.AreEqual(roomNumber, addedCatalog.RoomNumber, "Room number should match the provided room number");
            Assert.AreEqual(roomType, addedCatalog.RoomType, "Room type should match the provided room type");
            Assert.AreEqual(isBooked, addedCatalog.isBooked, "IsBooked status should match the provided status");
        }

        [TestMethod]
        public async Task GetCatalogAsync_ShouldReturnCatalogIfExists()
        {
            int catalogId = 8;
            int roomNumber = 102;
            string roomType = "Double";
            bool isBooked = true;

            await _catalogService.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            var retrievedCatalog = await _catalogService.GetCatalogAsync(catalogId);

            Assert.IsNotNull(retrievedCatalog, "Retrieved catalog should not be null");
            Assert.AreEqual(catalogId, retrievedCatalog.Id, "Catalog ID should match the provided ID");
            Assert.AreEqual(roomNumber, retrievedCatalog.RoomNumber, "Room number should match the provided room number");
            Assert.AreEqual(roomType, retrievedCatalog.RoomType, "Room type should match the provided room type");
            Assert.AreEqual(isBooked, retrievedCatalog.isBooked, "IsBooked status should match the provided status");
        }

        [TestMethod]
        public async Task UpdateCatalogAsync_ShouldUpdateExistingCatalog()
        {
            int catalogId = 9;
            int roomNumber = 103;
            string roomType = "Suite";
            bool isBooked = false;
            await _catalogService.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int newRoomNumber = 104;
            string newRoomType = "Penthouse";
            bool newIsBooked = true;

            await _catalogService.UpdateCatalogAsync(catalogId, newRoomNumber, newRoomType, newIsBooked);

            var updatedCatalog = await _catalogService.GetCatalogAsync(catalogId);

            Assert.IsNotNull(updatedCatalog, "Updated catalog should not be null");
            Assert.AreEqual(catalogId, updatedCatalog.Id, "Catalog ID should remain unchanged after update");
            Assert.AreEqual(newRoomNumber, updatedCatalog.RoomNumber, "Room number should be updated");
            Assert.AreEqual(newRoomType, updatedCatalog.RoomType, "Room type should be updated");
            Assert.AreEqual(newIsBooked, updatedCatalog.isBooked, "IsBooked status should be updated");
        }

        [TestMethod]
        public async Task DeleteCatalogAsync_ShouldDeleteExistingCatalog()
        {
            int catalogId = 10;
            int roomNumber = 105;
            string roomType = "Single";
            bool isBooked = false;
            await _catalogService.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            await _catalogService.DeleteCatalogAsync(catalogId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _catalogService.GetCatalogAsync(catalogId), "This catalog does not exist!");
        }

        [TestMethod]
        public async Task GetAllCatalogsAsync_ShouldReturnAllCatalogs()
        {
            var catalogs = new List<(int Id, int RoomNumber, string RoomType, bool IsBooked)>
            {
                (5, 106, "Single", false),
                (6, 107, "Double", true)
            };

            foreach (var catalog in catalogs)
            {
                await _catalogService.AddCatalogAsync(catalog.Id, catalog.RoomNumber, catalog.RoomType, catalog.IsBooked);
            }

            var allCatalogs = await _catalogService.GetAllCatalogsAsync();

            foreach (var catalog in catalogs)
            {
                Assert.IsTrue(allCatalogs.ContainsKey(catalog.Id), $"Catalog with ID {catalog.Id} should be in the retrieved list");
                var retrievedCatalog = allCatalogs[catalog.Id];
                Assert.AreEqual(catalog.RoomNumber, retrievedCatalog.RoomNumber, $"Room number should be {catalog.RoomNumber}");
                Assert.AreEqual(catalog.RoomType, retrievedCatalog.RoomType, $"Room type should be {catalog.RoomType}");
                Assert.AreEqual(catalog.IsBooked, retrievedCatalog.isBooked, $"IsBooked status should be {catalog.IsBooked}");
            }
        }
    }

    [TestClass]
    public class StateServiceTests
    {
        private string _connectionString;
        private IDataRepository _dataRepository;
        private ICatalogService _catalogService;
        private IStateService _stateService;

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Task2_v1.Properties.Settings.TestHotelConnectionString"].ConnectionString;
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            _dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            _catalogService = ServiceFactory.CreateCatalogService(_dataRepository);
            _stateService = ServiceFactory.CreateStateService(_dataRepository);
        }
        private async Task InitializeTestCatalogAsync(int catalogId)
        {
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;
            await _catalogService.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);
        }

        [TestMethod]
        public async Task AddStateAsync_ShouldAddStateToDatabase()
        {
            int catalogId = 103;
            await InitializeTestCatalogAsync(catalogId);

            int stateId = 5;
            int roomId = catalogId; 
            int price = 100;

            await _stateService.AddStateAsync(stateId, roomId, price);

            var addedState = await _stateService.GetStateAsync(stateId);
            Assert.IsNotNull(addedState, "State should be added to the database");
            Assert.AreEqual(stateId, addedState.Id, "State ID should match the provided ID");
            Assert.AreEqual(roomId, addedState.RoomCatalogId, "Room ID should match the provided Room ID");
            Assert.AreEqual(price, addedState.Price, "Price should match the provided price");
        }

        [TestMethod]
        public async Task GetStateAsync_ShouldReturnStateIfExists()
        {
            int catalogId = 104;
            await InitializeTestCatalogAsync(catalogId);

            int stateId = 6;
            int roomId = catalogId;
            int price = 200;

            await _stateService.AddStateAsync(stateId, roomId, price);

            var retrievedState = await _stateService.GetStateAsync(stateId);

            Assert.IsNotNull(retrievedState, "Retrieved state should not be null");
            Assert.AreEqual(stateId, retrievedState.Id, "State ID should match the provided ID");
            Assert.AreEqual(roomId, retrievedState.RoomCatalogId, "Room ID should match the provided Room ID");
            Assert.AreEqual(price, retrievedState.Price, "Price should match the provided price");
        }

        [TestMethod]
        public async Task UpdateStateAsync_ShouldUpdateExistingState()
        {
            int catalogId = 101;
            await InitializeTestCatalogAsync(catalogId);

            int stateId = 7;
            int roomId = catalogId;
            int price = 300;
            await _stateService.AddStateAsync(stateId, roomId, price);

            int newCatalogId = 102;
            await InitializeTestCatalogAsync(newCatalogId);

            int newRoomId = newCatalogId;
            int newPrice = 400;

            await _stateService.UpdateStateAsync(stateId, newRoomId, newPrice);

            var updatedState = await _stateService.GetStateAsync(stateId);

            Assert.IsNotNull(updatedState, "Updated state should not be null");
            Assert.AreEqual(stateId, updatedState.Id, "State ID should remain unchanged after update");
            Assert.AreEqual(newRoomId, updatedState.RoomCatalogId, "Room ID should be updated");
            Assert.AreEqual(newPrice, updatedState.Price, "Price should be updated");
        }

        [TestMethod]
        public async Task DeleteStateAsync_ShouldDeleteExistingState()
        {
            int catalogId = 105;
            await InitializeTestCatalogAsync(catalogId);

            int stateId = 8;
            int roomId = catalogId;
            int price = 500;
            await _stateService.AddStateAsync(stateId, roomId, price);

            await _stateService.DeleteStateAsync(stateId);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _stateService.GetStateAsync(stateId), "This state does not exist!");
        }

        [TestMethod]
        public async Task GetAllStatesAsync_ShouldReturnAllStates()
        {
            int catalogId1 = 106;
            int catalogId2 = 107;
            await InitializeTestCatalogAsync(catalogId1);
            await InitializeTestCatalogAsync(catalogId2);

            var states = new List<(int Id, int RoomId, int Price)>
            {
                (11, catalogId1, 600),
                (12, catalogId2, 700)
            };

            foreach (var state in states)
            {
                await _stateService.AddStateAsync(state.Id, state.RoomId, state.Price);
            }

            var allStates = await _stateService.GetAllStatesAsync();

            foreach (var state in states)
            {
                Assert.IsTrue(allStates.ContainsKey(state.Id), $"State with ID {state.Id} should be in the retrieved list");
                var retrievedState = allStates[state.Id];
                Assert.AreEqual(state.RoomId, retrievedState.RoomCatalogId, $"Room ID should be {state.RoomId}");
                Assert.AreEqual(state.Price, retrievedState.Price, $"Price should be {state.Price}");
            }
        }
    }

    [TestClass]
    public class EventServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
