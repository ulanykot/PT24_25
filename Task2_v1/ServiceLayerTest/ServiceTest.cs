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
        private ICatalogService _catalogService;
        private IStateService _stateService;

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Task2_v1.Properties.Settings.TestHotelConnectionString"].ConnectionString;
            var dataContext = DataContextFactory.CreateContext(_connectionString);
            _dataRepository = DataRepositoryFactory.CreateDatabase(dataContext);
            _userService = ServiceFactory.CreateUserService(_dataRepository); // Use ServiceFactory to create UserService
            _catalogService = ServiceFactory.CreateCatalogService(_dataRepository); // Use ServiceFactory to create CatalogService
            _stateService = ServiceFactory.CreateStateService(_dataRepository); // Use ServiceFactory to create StateService
        }

        private async Task InitializeTestStateAndCatalogAsync(int catalogId, int stateId)
        {
            int roomNumber = 101;
            string roomType = "Single";
            bool isBooked = false;
            await _catalogService.AddCatalogAsync(catalogId, roomNumber, roomType, isBooked);

            int roomId = catalogId;
            int price = 100;
            await _stateService.AddStateAsync(stateId, roomId, price);
        }

        [TestMethod]
        public async Task AddUserAsync_ShouldAddUserToDatabase()
        {
            int catalogId = 20;
            int stateId = 20;
            await InitializeTestStateAndCatalogAsync(catalogId, stateId);

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
            int catalogId = 21;
            int stateId = 21;
            await InitializeTestStateAndCatalogAsync(catalogId, stateId);

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
            int catalogId = 22;
            int stateId = 22;
            await InitializeTestStateAndCatalogAsync(catalogId, stateId);

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
            int catalogId = 23;
            int stateId = 23;
            await InitializeTestStateAndCatalogAsync(catalogId, stateId);

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
            int catalogId = 24;
            int stateId = 24;
            await InitializeTestStateAndCatalogAsync(catalogId, stateId);

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
    public class CatalogServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    [TestClass]
    public class StateServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
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
