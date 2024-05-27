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
        IUserModelOperation operation = IUserModelOperation.CreateModelOperation(new FakeUserCRUD());

        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(1, "Alice", "alice@example.com", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(2, "Bob", "bob@example.com", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(3, "Charlie", "charlie@example.com", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(4, "Diana", "diana@example.com", operation, _informer));
        viewModel.Users.Add(IUserDetailViewModel.CreateViewModel(5, "Eve", "eve@example.com", operation, _informer));
    }

    public void GenerateProductModels(IProductMasterViewModel viewModel)
    {
        ICatalogModelOperation operation = ICatalogModelOperation.CreateModelOperation(new FakeProductCRUD());

        viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(1, "Apples", 61.99, operation, _informer));
        viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(2, "Oranges", 32.99, operation, _informer));
        viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(3, "Sausage", 2.99, operation, _informer));
        viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(4, "Tomato", 9.99, operation, _informer));
        viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(5, "Ice cream", 1.99, operation, _informer));
        viewModel.Products.Add(IProductDetailViewModel.CreateViewModel(6, "Pizza", 39.99, operation, _informer));
    }

    public void GenerateStateModels(IStateMasterViewModel viewModel)
    {
        IStateModelOperation operation = IStateModelOperation.CreateModelOperation(new FakeStateCRUD());

        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(1, 1, 3, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(2, 2, 9, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(3, 3, 11, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(4, 4, 12, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(5, 5, 22, operation, _informer));
        viewModel.States.Add(IStateDetailViewModel.CreateViewModel(6, 6, 33, operation, _informer));
    }

    public void GenerateEventModels(IEventMasterViewModel viewModel)
    {
        IEventModelOperation operation = IEventModelOperation.CreateModelOperation(new FakeEventCRUD());

        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(1, 1, 1, DateTime.Now, "SupplyEvent", 10, operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(2, 2, 2, DateTime.Now, "SupplyEvent", 123, operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(3, 3, 3, DateTime.Now, "SupplyEvent", 3, operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(4, 4, 4, DateTime.Now, "SupplyEvent", 5, operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(5, 5, 5, DateTime.Now, "SupplyEvent", 15, operation, _informer));
        viewModel.Events.Add(IEventDetailViewModel.CreateViewModel(6, 6, 6, DateTime.Now, "SupplyEvent", 23, operation, _informer));

    }
}
