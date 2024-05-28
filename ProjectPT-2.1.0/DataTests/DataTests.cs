using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.API;
using Data.Database;
using System;

namespace DataTests;

[TestClass]
[DeploymentItem("TestHotelDatabase.mdf")]
public class DataTests
{
    private static string connectionString;

    private readonly IDataRepository _dataRepository = IDataRepository.CreateDatabase(IDataContext.CreateContext(connectionString));

    [ClassInitialize]
    public static void ClassInitializeMethod(TestContext context)
    {
        string _DBRelativePath = @"TestHotelDatabase.mdf";
        string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
        FileInfo _databaseFile = new FileInfo(_DBPath);
        Assert.IsTrue(_databaseFile.Exists, $"{Environment.CurrentDirectory}");
        connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
    }
    [TestMethod]
    public async Task UserTests()
    {
        int userId = 5;

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
        int catalogId = 12;

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
        int eventId = 4;
        int userId = 4;
        int catalogId = 4;
        int stateId = 4;

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

    [TestMethod]
    public async Task EventsActionTest()
    {
        int userId1 = 5;
        int stateId1 = 5;
        int eventId1 = 5;
        int catalogId1 = 5;

        await _dataRepository.AddCatalogAsync(catalogId1, 124, "Single", false);
        await _dataRepository.AddStateAsync(stateId1, catalogId1, 100);
        await _dataRepository.AddUserAsync(userId1, "John", "Doe", "Guest");
        await _dataRepository.AddEventAsync(eventId1, stateId1, userId1, DateTime.Now, DateTime.Now.AddDays(3), "CheckIn");

        Assert.AreEqual(100, (await _dataRepository.GetStateAsync(stateId1)).Price);

        await _dataRepository.DeleteEventAsync(eventId1);
        await _dataRepository.DeleteStateAsync(stateId1);
        await _dataRepository.DeleteCatalogAsync(catalogId1);
        await _dataRepository.DeleteUserAsync(userId1);

        int userId2 = 5;
        int stateId2 = 5;
        int eventId2 = 5;
        int catalogId2 = 5;

        await _dataRepository.AddCatalogAsync(catalogId2, 124, "Single", false);
        await _dataRepository.AddStateAsync(stateId2, catalogId2, 100);
        await _dataRepository.AddUserAsync(userId2, "John", "Doe", "Guest");


        await _dataRepository.DeleteStateAsync(stateId2);
        await _dataRepository.DeleteCatalogAsync(catalogId2);
        await _dataRepository.DeleteUserAsync(userId2);

        int userId3 = 5;
        int stateId3 = 5;
        int eventId3 = 5;
        int catalogId3 = 5;

        await _dataRepository.AddCatalogAsync(catalogId3, 122, "Suite", false);
        await _dataRepository.AddStateAsync(stateId3, catalogId3, 200);
        await _dataRepository.AddUserAsync(userId3, "Jackson", "Doe", "Guest");


        await _dataRepository.DeleteStateAsync(stateId3);
        await _dataRepository.DeleteCatalogAsync(catalogId3);
        await _dataRepository.DeleteUserAsync(userId3);

        int userId4 = 5;
        int stateId4 = 5;
        int eventId4 = 5;
        int catalogId4 = 5;

        await _dataRepository.AddCatalogAsync(catalogId4, 122, "Double", false);
        await _dataRepository.AddStateAsync(stateId4, catalogId4, 160);
        await _dataRepository.AddUserAsync(userId4, "Michael", "Black", "Staff");


        await _dataRepository.DeleteStateAsync(stateId4);
        await _dataRepository.DeleteCatalogAsync(catalogId4);
        await _dataRepository.DeleteUserAsync(userId4);

        int userId5 = 5;
        int stateId5 = 5;
        int checkInEventId5 = 5;
        int checkOutEventId5 = 6;
        int catalogId5 = 5;

        await _dataRepository.AddCatalogAsync(catalogId5, 127, "Suite", false);
        await _dataRepository.AddStateAsync(stateId5, catalogId5, 240);
        await _dataRepository.AddUserAsync(userId5, "Angelina", "Jolie", "Guest");

        await _dataRepository.AddEventAsync(checkInEventId5, stateId5, userId5, DateTime.Now, DateTime.Now.AddDays(4), "CheckIn");
        await _dataRepository.AddEventAsync(checkOutEventId5, stateId5, userId5, DateTime.Now.AddDays(-4), DateTime.Now, "CheckOut");

        await _dataRepository.DeleteEventAsync(checkInEventId5);
        await _dataRepository.DeleteEventAsync(checkOutEventId5);
        await _dataRepository.DeleteStateAsync(stateId5);
        await _dataRepository.DeleteCatalogAsync(catalogId5);
        await _dataRepository.DeleteUserAsync(userId5);

        int userId6 = 7;
        int stateId6 = 7;
        int eventId6 = 7;
        int catalogId6 = 7;

        await _dataRepository.AddCatalogAsync(catalogId6, 129, "Double", true);
        await _dataRepository.AddStateAsync(stateId6, catalogId6, 160);
        await _dataRepository.AddUserAsync(userId6, "bro", "what", "Admin");

        await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataRepository.AddEventAsync(eventId6, stateId6, userId6, DateTime.Now, DateTime.Now.AddDays(3), "CheckIn"));

        await _dataRepository.DeleteStateAsync(stateId6);
        await _dataRepository.DeleteCatalogAsync(catalogId6);
        await _dataRepository.DeleteUserAsync(userId6);

        int userId7 = 8;
        int stateId7 = 8;
        int eventId7 = 8;
        int catalogId7 = 8;

        await _dataRepository.AddCatalogAsync(catalogId7, 129, "Double", true);
        await _dataRepository.AddStateAsync(stateId7, catalogId7, 160);
        await _dataRepository.AddUserAsync(userId7, "need", "sleep", "Guest");

        await _dataRepository.AddEventAsync(eventId7, stateId7, userId7, DateTime.Now, DateTime.Now, "CheckOut");

        await _dataRepository.DeleteEventAsync(eventId7);
        await _dataRepository.DeleteStateAsync(stateId7);
        await _dataRepository.DeleteCatalogAsync(catalogId7);
        await _dataRepository.DeleteUserAsync(userId7);
    }
}
