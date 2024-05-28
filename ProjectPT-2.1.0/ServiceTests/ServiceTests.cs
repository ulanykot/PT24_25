using Data.API;
using Service.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceTests;

[TestClass]
public class ServiceTests
{
    private readonly IDataRepository _repository = new FakeDataRepository();

    [TestMethod]
    public async Task UserServiceTests()
    {
        IUserService userCrud = IUserService.CreateUserService(this._repository);

        await userCrud.AddUserAsync(1, "Maciek", "Kowal", "guest");

        IUserService user = await userCrud.GetUserAsync(1);

        Assert.IsNotNull(user);
        Assert.AreEqual(1, user.Id);
        Assert.AreEqual("Maciek", user.FirstName);
        Assert.AreEqual("Kowal", user.LastName);
        Assert.AreEqual("guest", user.UserType);

        Assert.IsNotNull(await userCrud.GetAllUsersAsync());
        Assert.IsTrue(await userCrud.GetUsersCountAsync() > 0);

        await userCrud.UpdateUserAsync(1, "Marcin", "Kowals", "admin");

        IUserService updatedUser = await userCrud.GetUserAsync(1);

        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(1, updatedUser.Id);
        Assert.AreEqual("Marcin", updatedUser.FirstName);
        Assert.AreEqual("Kowals", updatedUser.LastName);
        Assert.AreEqual("admin", updatedUser.UserType);

        await userCrud.DeleteUserAsync(1);
    }

    [TestMethod]
    public async Task ProductServiceTests()
    {
        ICatalogService catalogCrud = ICatalogService.CreateCatalogService(this._repository);

        await catalogCrud.AddCatalogAsync(1, 100, "Single", false);

        ICatalogService catalog = await catalogCrud.GetCatalogAsync(1);

        Assert.IsNotNull(catalog);
        Assert.AreEqual(1, catalog.Id);
        Assert.AreEqual(100, catalog.RoomNumber);
        Assert.AreEqual("Single", catalog.RoomType);
        Assert.AreEqual(false, catalog.isBooked);

        Assert.IsNotNull(await catalogCrud.GetAllCatalogsAsync());
        Assert.IsTrue(await catalogCrud.GetCatalogsCountAsync() > 0);

        await catalogCrud.UpdateCatalogAsync(1, 101, "Double", false);

        ICatalogService updatedCatalog = await catalogCrud.GetCatalogAsync(1);

        Assert.IsNotNull(updatedCatalog);
        Assert.AreEqual(1, updatedCatalog.Id);
        Assert.AreEqual(101, updatedCatalog.RoomNumber);
        Assert.AreEqual("Double", updatedCatalog.RoomType);
        Assert.AreEqual(false, updatedCatalog.isBooked);

        await catalog.DeleteCatalogAsync(1);
    }

    [TestMethod]
    public async Task StateServiceTests()
    {
        ICatalogService catalogCrud = ICatalogService.CreateCatalogService(this._repository);

        await catalogCrud.AddCatalogAsync(1, 101, "Double", false);

        ICatalogService catalog = await catalogCrud.GetCatalogAsync(1);

        IStateService stateCrud = IStateService.CreateStateService(this._repository);

        await stateCrud.AddStateAsync(1, 1, 140);

        IStateService state = await stateCrud.GetStateAsync(1);

        Assert.IsNotNull(state);
        Assert.AreEqual(1, state.Id);
        Assert.AreEqual(1, state.RoomCatalogId);
        Assert.AreEqual(140, state.Price);

        await stateCrud.UpdateStateAsync(1, 1, 150);

        IStateService updatedState = await stateCrud.GetStateAsync(1);

        Assert.IsNotNull(updatedState);
        Assert.AreEqual(1, updatedState.Id);
        Assert.AreEqual(1, updatedState.RoomCatalogId);
        Assert.AreEqual(150, updatedState.Price);

        await stateCrud.DeleteStateAsync(1);
        await catalogCrud.DeleteCatalogAsync(1);
    }

    [TestMethod]
    public async Task CheckInEventServiceTests()
    {
        ICatalogService catalogCrud = ICatalogService.CreateCatalogService(this._repository);

        await catalogCrud.AddCatalogAsync(1, 101, "Double", false);

        ICatalogService catalog = await catalogCrud.GetCatalogAsync(1);

        IStateService stateCrud = IStateService.CreateStateService(this._repository);

        await stateCrud.AddStateAsync(1, catalog.Id, 120);

        IStateService state = await stateCrud.GetStateAsync(1);

        IUserService userCrud = IUserService.CreateUserService(this._repository);

        await userCrud.AddUserAsync(1, "Pan","Jan", "guest");

        IUserService user = await userCrud.GetUserAsync(1);

        IEventService eventCrud = IEventService.CreateEventService(this._repository);

        await eventCrud.AddEventAsync(1, state.Id, user.Id, DateTime.Now, DateTime.Now.AddDays(2), "CheckIn");

        user = await userCrud.GetUserAsync(1);
        state = await stateCrud.GetStateAsync(1);

        await eventCrud.DeleteEventAsync(1);
        await stateCrud.DeleteStateAsync(1);
        await catalogCrud.DeleteCatalogAsync(1);
        await userCrud.DeleteUserAsync(1);
    }

    [TestMethod]
    public async Task CheckOutEventServiceTests()
    {
        ICatalogService catalogCrud = ICatalogService.CreateCatalogService(this._repository);

        await catalogCrud.AddCatalogAsync(2, 101, "Double", true);

        ICatalogService catalog = await catalogCrud.GetCatalogAsync(2);

        IStateService stateCrud = IStateService.CreateStateService(this._repository);

        await stateCrud.AddStateAsync(2, catalog.Id, 130);

        IStateService state = await stateCrud.GetStateAsync(2);

        IUserService userCrud = IUserService.CreateUserService(this._repository);

        await userCrud.AddUserAsync(2, "Anna", "Chrzan", "guest");

        IUserService user = await userCrud.GetUserAsync(2);

        IEventService eventCrud = IEventService.CreateEventService(this._repository);

        await eventCrud.AddEventAsync(2, state.Id, user.Id, DateTime.Now.AddDays(-3), DateTime.Now, "CheckIn");

        await eventCrud.AddEventAsync(3, state.Id, user.Id, DateTime.Now.AddDays(-3), DateTime.Now, "CheckOut");

        user = await userCrud.GetUserAsync(2);
        state = await stateCrud.GetStateAsync(2);

        await eventCrud.DeleteEventAsync(2);
        await eventCrud.DeleteEventAsync(3);
        await stateCrud.DeleteStateAsync(2);
        await catalogCrud.DeleteCatalogAsync(2);
        await userCrud.DeleteUserAsync(2);
    }
}
