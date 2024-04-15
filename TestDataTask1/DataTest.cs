using DataTask1;
using TestTask1;

namespace TestDataTask1
{
    [TestClass]
    public class DataTest
    {
        private IDataRepository repository;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = new DataContext();
            repository = new DataRepository(context);
        }

        [TestMethod]
        public void TestAddUser()
        {
            User user = TestDataGeneratorStatic.GenerateStaticUser(1);
            repository.AddUser(user);
            Assert.IsTrue(repository.GetAllUsers().Contains(user), "User should be added to the list.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No user found.")]
        public void TestGetUser_ThrowsExceptionIfUserNotFound()
        {
            var user = repository.GetUser(999);
        }

        [TestMethod]
        public void TestIsUserInEvent_ReturnsFalseWhenUserNotInEvent()
        {
            User user = TestDataGeneratorStatic.GenerateStaticUser(1);
            var result = repository.IsUserInEvent(user);
            Assert.IsFalse(result, "User should not be in any events.");
        }
    }
}