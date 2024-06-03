using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.API;

namespace DataTests;

[TestClass]
[DeploymentItem("TestDatabase.mdf")]
public class DataTests
{
    private static string connectionString;

    private readonly IDataRepository _dataRepository = IDataRepository.CreateDatabase(IDataContext.CreateContext(connectionString));

    [ClassInitialize]
    public static void ClassInitializeMethod(TestContext context)
    {
        string _DBRelativePath = @"TestDatabase.mdf";
        string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
        FileInfo _databaseFile = new FileInfo(_DBPath);
        Assert.IsTrue(_databaseFile.Exists, $"{Environment.CurrentDirectory}");
        connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
    }
    [TestMethod]
    public async Task UserTests()
    {
        int userId = 76;

        await _dataRepository.AddUserAsync(userId, "John", "Doe", "Guest");

        IUser user = await _dataRepository.GetUserAsync(userId);

        Assert.IsNotNull(user);
        Assert.AreEqual(userId, user.Id);
        Assert.AreEqual("John", user.FirstName);
        Assert.AreEqual("Doe", user.LastName);
        Assert.AreEqual("Guest", user.UserType);

        Assert.IsNotNull(await _dataRepository.GetAllUsersAsync());
        Assert.IsTrue(await _dataRepository.GetUsersCountAsync() > 0);

        await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUserAsync(userId + 2));
        await _dataRepository.UpdateUserAsync(userId, "Jane", "Smith", "Staff");

        IUser userUpdated = await _dataRepository.GetUserAsync(userId);

        Assert.IsNotNull(userUpdated);
        Assert.AreEqual(userId, userUpdated.Id);
        Assert.AreEqual("Jane", userUpdated.FirstName);
        Assert.AreEqual("Smith", userUpdated.LastName);
        Assert.AreEqual("Staff", userUpdated.UserType);

        await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.UpdateUserAsync(userId + 2,
            "Jane", "Smith", "Staff"));

        await _dataRepository.DeleteUserAsync(userId);
        await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.GetUserAsync(userId));
        await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.DeleteUserAsync(userId));
    }

    [TestMethod]
    public async Task CatalogTests()
    {
        int catalogId = 17;

        await _dataRepository.AddCatalogAsync(catalogId, 101, "Single", false);

        ICatalog catalog = await _dataRepository.GetCatalogAsync(catalogId);

        Assert.IsNotNull(catalog);
        Assert.AreEqual(catalogId, catalog.Id);
        Assert.AreEqual(101, catalog.RoomNumber);
        Assert.AreEqual("Single", catalog.RoomType);
        Assert.AreEqual(false, catalog.isBooked);

        Assert.IsNotNull(await _dataRepository.GetAllCatalogsAsync());
        Assert.IsTrue(await _dataRepository.GetCatalogsCountAsync() > 0);

        await _dataRepository.UpdateCatalogAsync(catalogId, 102, "Suite", true);

        ICatalog catalogUpdated = await _dataRepository.GetCatalogAsync(catalogId);

        Assert.IsNotNull(catalogUpdated);
        Assert.AreEqual(catalogId, catalogUpdated.Id);
        Assert.AreEqual(102, catalogUpdated.RoomNumber);
        Assert.AreEqual("Suite", catalogUpdated.RoomType);
        Assert.AreEqual(true, catalogUpdated.isBooked);

        await _dataRepository.DeleteCatalogAsync(catalogId);
    }

    [TestMethod]
    public async Task StateTests()
    {
        int catalogId = 28;
        int stateId = 7;

        await _dataRepository.AddCatalogAsync(catalogId, 104, "Double", false);

        ICatalog product = await _dataRepository.GetCatalogAsync(catalogId);

        await _dataRepository.AddStateAsync(stateId, catalogId, 120);

        IState state = await _dataRepository.GetStateAsync(stateId);

        Assert.IsNotNull(state);
        Assert.AreEqual(stateId, state.Id);
        Assert.AreEqual(catalogId, state.RoomCatalogId);
        Assert.AreEqual(120, state.Price);

        Assert.IsNotNull(await _dataRepository.GetAllStatesAsync());
        Assert.IsTrue(await _dataRepository.GetStatesCountAsync() > 0);

        await _dataRepository.UpdateStateAsync(stateId, catalogId, 150);

        IState stateUpdated = await _dataRepository.GetStateAsync(stateId);

        Assert.IsNotNull(stateUpdated);
        Assert.AreEqual(stateId, stateUpdated.Id);
        Assert.AreEqual(catalogId, stateUpdated.RoomCatalogId);
        Assert.AreEqual(150, stateUpdated.Price);


        await _dataRepository.DeleteStateAsync(stateId);
        await _dataRepository.DeleteCatalogAsync(catalogId);
    }
    [TestMethod]
    public async Task EventTests()
    {
        int eventId = 24;
        int userId = 24;
        int catalogId = 24;
        int stateId = 24;

        await _dataRepository.AddCatalogAsync(catalogId, 109, "Suite", false);
        await _dataRepository.AddStateAsync(stateId, catalogId, 130);
        await _dataRepository.AddUserAsync(userId, "Michael", "Pitt", "Guest");

        ICatalog catalog = await _dataRepository.GetCatalogAsync(catalogId);
        IState state = await _dataRepository.GetStateAsync(stateId);
        IUser user = await _dataRepository.GetUserAsync(userId);

        await _dataRepository.AddEventAsync(eventId, stateId, userId, DateTime.Now, DateTime.Now.AddDays(2), "CheckIn");

        IEvent anotherEvent = await _dataRepository.GetEventAsync(eventId);

        Assert.IsNotNull(anotherEvent);
        Assert.AreEqual(eventId, anotherEvent.Id);
        Assert.AreEqual(stateId, anotherEvent.StateId);
        Assert.AreEqual(userId, anotherEvent.UserId);

        Assert.IsNotNull(await _dataRepository.GetAllEventsAsync());
        Assert.IsTrue(await _dataRepository.GetEventsCountAsync() > 0);

        await _dataRepository.UpdateEventAsync(eventId, stateId, userId, DateTime.Now.AddDays(2), DateTime.Now.AddDays(4), "CheckIn");

        IEvent eventUpdated = await _dataRepository.GetEventAsync(eventId);

        Assert.IsNotNull(eventUpdated);
        Assert.AreEqual(eventId, eventUpdated.Id);
        Assert.AreEqual(stateId, eventUpdated.StateId);
        Assert.AreEqual(userId, eventUpdated.UserId);

        await _dataRepository.DeleteEventAsync(eventId);
        await _dataRepository.DeleteStateAsync(stateId);
        await _dataRepository.DeleteCatalogAsync(catalogId);
        await _dataRepository.DeleteUserAsync(userId);
    }

   
}

