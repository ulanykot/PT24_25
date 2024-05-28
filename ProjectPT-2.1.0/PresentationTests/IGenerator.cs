using Presentation.ViewModel;

namespace PresentationTests;

public interface IGenerator
{
    void GenerateUserModels(IUserMasterViewModel viewModel);

    void GenerateCatalogModels(ICatalogMasterViewModel viewModel);

    void GenerateStateModels(IStateMasterViewModel viewModel);

    void GenerateEventModels(IEventMasterViewModel viewModel);
}