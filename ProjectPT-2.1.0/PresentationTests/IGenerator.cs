using PresentationViewModel;

namespace PresentationTests;

public interface IGenerator
{
    void GenerateUserModels(IUserMasterViewModel viewModel);

    void GenerateCatalogModels(IProductMasterViewModel viewModel);

    void GenerateStateModels(IStateMasterViewModel viewModel);

    void GenerateEventModels(IEventMasterViewModel viewModel);
}