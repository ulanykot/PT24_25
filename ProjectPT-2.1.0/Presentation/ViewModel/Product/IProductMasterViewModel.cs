using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PresentationViewModel;

public interface IProductMasterViewModel
{
    static IProductMasterViewModel CreateViewModel(ICatalogModelOperation operation, IErrorInformer informer)
    {
        return new ProductMasterViewModel(operation, informer);
    }

    ICommand CreateProduct { get; set; }

    ICommand RemoveProduct { get; set; }

    ObservableCollection<IProductDetailViewModel> Products { get; set; }

    int Id { get; set; }

    int RoomNumber { get; set; }

    string RoomType { get; set; }

    bool IsBooked { get; set; }

    bool IsProductSelected { get; set; }

    Visibility IsProductDetailVisible { get; set; }

    IProductDetailViewModel SelectedDetailViewModel { get; set; }
}
