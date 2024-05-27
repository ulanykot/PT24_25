using DataLayer.API;
using DataLayer.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
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
    }
}
