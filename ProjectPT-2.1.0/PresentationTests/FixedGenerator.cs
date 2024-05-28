using Presentation;
using Presentation.Model.API;
using Presentation.ViewModel;
using PresentationTest;

namespace PresentationTests;

internal class FixedGenerator : IGenerator
{
    private readonly IErrorInformer _informer = new TextErrorInformer();

    public void GenerateUserModels(IUserMasterViewModel viewModel)
    {
        IUserModelOperation operation = IUserModelOperation.CreateModelOperation(new FakeUserService());

        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(1, "Alice", "Smith", "Standard", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(2, "Bob", "Johnson", "Standard", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(3, "Charlie", "Williams", "Standard", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(4, "Diana", "Brown", "Standard", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(5, "Eve", "Davis", "Standard", operation, _informer));
    }

    public void GenerateCatalogModels(ICatalogMasterViewModel viewModel)
    {
        ICatalogModelOperation operation = ICatalogModelOperation.CreateModelOperation(new FakeCatalogService());

        viewModel.Catalogs.Add(ICatalogDetailViewModel.CreateViewModel(1, 101, "Suite", true, operation, _informer));
        viewModel.Catalogs.Add(ICatalogDetailViewModel.CreateViewModel(2, 102, "Single", false, operation, _informer));
        viewModel.Catalogs.Add(ICatalogDetailViewModel.CreateViewModel(3, 103, "Suite", true, operation, _informer));
        viewModel.Catalogs.Add(ICatalogDetailViewModel.CreateViewModel(4, 104, "Double", false, operation, _informer));
        viewModel.Catalogs.Add(ICatalogDetailViewModel.CreateViewModel(5, 105, "Single", true, operation, _informer));
        viewModel.Catalogs.Add(ICatalogDetailViewModel.CreateViewModel(6, 106, "Suite", true, operation, _informer));
    }

    public void GenerateStateModels(IStateMasterViewModel viewModel)
    {
        IStateModelOperation operation = IStateModelOperation.CreateModelOperation(new FakeStateService());

        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(1, 1, 3, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(2, 2, 9, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(3, 3, 11, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(4, 4, 12, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(5, 5, 22, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(6, 6, 33, operation, _informer));
    }

    public void GenerateEventModels(IEventMasterViewModel viewModel)
    {
        IEventModelOperation operation = IEventModelOperation.CreateModelOperation(new FakeEventService());

        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(1, 1, 1, DateTime.Now, DateTime.Now.AddDays(2), "CheckIn", operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(2, 2, 2, DateTime.Now, DateTime.Now.AddDays(3), "CheckIn", operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(3, 3, 3, DateTime.Now.AddDays(2), DateTime.Now.AddDays(5), "CheckIn", operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(4, 4, 4, DateTime.Now, DateTime.Now.AddDays(2), "CheckIn", operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(5, 5, 5, DateTime.Now, DateTime.Now.AddDays(4), "CheckIn", operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(6, 6, 6, DateTime.Now.AddDays(7), DateTime.Now.AddDays(12), "CheckIn", operation, _informer));

    }
}
