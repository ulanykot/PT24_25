
using PresentationModel;
using PresentationViewModel;
using PresentationTest;
using Service.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PresentationTests;

[TestClass]
public class PresentationTests
{
    private readonly IErrorInformer _informer = new TextErrorInformer();

    [TestMethod]
    public void UserMasterViewModelTests()
    {
        IUserService fakeUserCrud = new FakeUserService();

        IUserModelOperation operation = IUserModelOperation.CreateModelOperation(fakeUserCrud);

        IUserMasterViewModel viewModel = IUserMasterViewModel.CreateViewModel(operation, _informer);

        viewModel.FirstName = "Test";
        viewModel.LastName = "Tester";
        viewModel.UserType = "Guest";

        Assert.IsNotNull(viewModel.CreateUser);
        Assert.IsNotNull(viewModel.RemoveUser);

        Assert.IsTrue(viewModel.CreateUser.CanExecute(null));

        Assert.IsTrue(viewModel.RemoveUser.CanExecute(null));
    }

    [TestMethod]
    public void UserDetailViewModelTests()
    {
        IUserService fakeUserCrud = new FakeUserService();

        IUserModelOperation operation = IUserModelOperation.CreateModelOperation(fakeUserCrud);

        IUserDetailViewModel detail = IUserDetailViewModel.CreateViewModel(1, "Test", "Tester", "Guest", operation, _informer);

        Assert.AreEqual(1, detail.Id);
        Assert.AreEqual("Test", detail.FirstName);
        Assert.AreEqual("Tester", detail.LastName);
        Assert.AreEqual("Guest", detail.UserType);

        Assert.IsTrue(detail.UpdateUser.CanExecute(null));
    }

    [TestMethod]
    public void CatalogMasterViewModelTests()
    {
        ICatalogService fakeCatalogCrud = new FakeCatalogService();

        ICatalogModelOperation operation = ICatalogModelOperation.CreateModelOperation(fakeCatalogCrud);

        IProductMasterViewModel master = IProductMasterViewModel.CreateViewModel(operation, _informer);

        master.RoomNumber = 101;
        master.RoomType = "Single";
        master.IsBooked = false;

        Assert.IsNotNull(master.CreateProduct);
        Assert.IsNotNull(master.RemoveProduct);

        Assert.IsTrue(master.CreateProduct.CanExecute(null));

        master.RoomNumber = -1;

        //Assert.IsFalse(master.CreateCatalog.CanExecute(null));

        Assert.IsTrue(master.RemoveProduct.CanExecute(null));
    }

    [TestMethod]
    public void CatalogDetailViewModelTests()
    {
        ICatalogService fakeCatalogCrud = new FakeCatalogService();

        ICatalogModelOperation operation = ICatalogModelOperation.CreateModelOperation(fakeCatalogCrud);

        IProductDetailViewModel detail = IProductDetailViewModel.CreateViewModel(1, 115, "Suite", false, operation, _informer);

        Assert.AreEqual(1, detail.Id);
        Assert.AreEqual(115, detail.RoomNumber);
        Assert.AreEqual("Suite", detail.RoomType);
        Assert.AreEqual(false, detail.IsBooked);

        Assert.IsTrue(detail.UpdateProduct.CanExecute(null));
    }

    [TestMethod]
    public void StateMasterViewModelTests()
    {
        IStateService fakeStateCrud = new FakeStateService();

        IStateModelOperation operation = IStateModelOperation.CreateModelOperation(fakeStateCrud);

        IStateMasterViewModel master = IStateMasterViewModel.CreateViewModel(operation, _informer);

        master.ProductId = 1;
        master.Price = 1;

        Assert.IsNotNull(master.CreateState);
        Assert.IsNotNull(master.RemoveState);

        Assert.IsTrue(master.CreateState.CanExecute(null));

        master.Price = -1;

        Assert.IsFalse(master.CreateState.CanExecute(null));

        Assert.IsTrue(master.RemoveState.CanExecute(null));
    }

    [TestMethod]
    public void StateDetailViewModelTests()
    {
        IStateService fakeStateCrud = new FakeStateService();

        IStateModelOperation operation = IStateModelOperation.CreateModelOperation(fakeStateCrud);

        IStateDetailViewModel detail = IStateDetailViewModel.CreateViewModel(1, 1, 100, operation, _informer);

        Assert.AreEqual(1, detail.Id);
        Assert.AreEqual(1, detail.ProductId);
        Assert.AreEqual(100, detail.Price);

        Assert.IsTrue(detail.UpdateState.CanExecute(null));
    }

    [TestMethod]
    public void EventMasterViewModelTests()
    {
        IEventService fakeEventCrud = new FakeEventService();

        IEventModelOperation operation = IEventModelOperation.CreateModelOperation(fakeEventCrud);

        IEventMasterViewModel master = IEventMasterViewModel.CreateViewModel(operation, _informer);

        master.StateId = 1;
        master.UserId = 1;

        Assert.IsNotNull(master.CheckInEvent);
        Assert.IsNotNull(master.CheckOutEvent);
        Assert.IsNotNull(master.RemoveEvent);

        Assert.IsTrue(master.CheckInEvent.CanExecute(null));
        Assert.IsTrue(master.CheckOutEvent.CanExecute(null));

        Assert.IsTrue(master.RemoveEvent.CanExecute(null));
    }

    [TestMethod]
    public void EventDetailViewModelTests()
    {
        IEventService fakeEventCrud = new FakeEventService();

        IEventModelOperation operation = IEventModelOperation.CreateModelOperation(fakeEventCrud);

        IEventDetailViewModel detail = IEventDetailViewModel.CreateViewModel(1, 1, 1, new DateTime(2001, 1, 1), new DateTime(2001, 1, 1),
            "CheckIn", operation, _informer);

        Assert.AreEqual(1, detail.Id);
        Assert.AreEqual(1, detail.StateId);
        Assert.AreEqual(1, detail.UserId);
        Assert.AreEqual(new DateTime(2001, 1, 1), detail.CheckIn);
        Assert.AreEqual(new DateTime(2001, 1, 1), detail.CheckOut);
        Assert.AreEqual("CheckIn", detail.Type);

        Assert.IsTrue(detail.UpdateEvent.CanExecute(null));
    }

    [TestMethod]
    public void DataFixedGenerationMethodTests()
    {
        IGenerator fixedGenerator = new FixedGenerator();

        IUserService fakeUserCrud = new FakeUserService();
        IUserModelOperation userOperation = IUserModelOperation.CreateModelOperation(fakeUserCrud);
        IUserMasterViewModel userViewModel = IUserMasterViewModel.CreateViewModel(userOperation, _informer);

        ICatalogService fakeProductCrud = new FakeCatalogService();
        ICatalogModelOperation catalogOperation = ICatalogModelOperation.CreateModelOperation(fakeProductCrud);
        IProductMasterViewModel catalogViewModel = IProductMasterViewModel.CreateViewModel(catalogOperation, _informer);


        IStateService fakeStateCrud = new FakeStateService();
        IStateModelOperation stateOperation = IStateModelOperation.CreateModelOperation(fakeStateCrud);
        IStateMasterViewModel stateViewModel = IStateMasterViewModel.CreateViewModel(stateOperation, _informer);

        IEventService fakeEventCrud = new FakeEventService();
        IEventModelOperation eventOperation = IEventModelOperation.CreateModelOperation(fakeEventCrud);
        IEventMasterViewModel eventViewModel = IEventMasterViewModel.CreateViewModel(eventOperation, _informer);

        fixedGenerator.GenerateUserModels(userViewModel);
        fixedGenerator.GenerateCatalogModels(catalogViewModel);
        fixedGenerator.GenerateStateModels(stateViewModel);
        fixedGenerator.GenerateEventModels(eventViewModel);

        Assert.AreEqual(5, userViewModel.Users.Count);
        Assert.AreEqual(6, catalogViewModel.Products.Count);
        Assert.AreEqual(6, stateViewModel.States.Count);
        Assert.AreEqual(6, eventViewModel.Events.Count);
    }

    [TestMethod]
    public void DataRandomGenerationMethodTests()
    {
        IGenerator randomGenerator = new RandomGenerator();

        IUserService fakeUserCrud = new FakeUserService();
        IUserModelOperation userOperation = IUserModelOperation.CreateModelOperation(fakeUserCrud);
        IUserMasterViewModel userViewModel = IUserMasterViewModel.CreateViewModel(userOperation, _informer);

        ICatalogService fakeProductCrud = new FakeCatalogService();
        ICatalogModelOperation catalogOperation = ICatalogModelOperation.CreateModelOperation(fakeProductCrud);
        IProductMasterViewModel catalogViewModel = IProductMasterViewModel.CreateViewModel(catalogOperation, _informer);


        IStateService fakeStateCrud = new FakeStateService();
        IStateModelOperation stateOperation = IStateModelOperation.CreateModelOperation(fakeStateCrud);
        IStateMasterViewModel stateViewModel = IStateMasterViewModel.CreateViewModel(stateOperation, _informer);

        IEventService fakeEventCrud = new FakeEventService();
        IEventModelOperation eventOperation = IEventModelOperation.CreateModelOperation(fakeEventCrud);
        IEventMasterViewModel eventViewModel = IEventMasterViewModel.CreateViewModel(eventOperation, _informer);

        randomGenerator.GenerateUserModels(userViewModel);
        randomGenerator.GenerateCatalogModels(catalogViewModel);
        randomGenerator.GenerateStateModels(stateViewModel);
        randomGenerator.GenerateEventModels(eventViewModel);

        Assert.AreEqual(10, userViewModel.Users.Count);
        Assert.AreEqual(10, catalogViewModel.Products.Count);
        Assert.AreEqual(10, stateViewModel.States.Count);
        Assert.AreEqual(10, eventViewModel.Events.Count);
    }
}
