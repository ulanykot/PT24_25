using System.Windows.Input;
using Presentation.Model.API;

namespace PresentationViewModel;

public interface IProductDetailViewModel
{
    static IProductDetailViewModel CreateViewModel(int id, int roomNumber, string roomType, bool isBooked,
         ICatalogModelOperation model, IErrorInformer informer)
    {
        return new ProductDetailViewModel(id, roomNumber, roomType, isBooked, model, informer);
    }

    ICommand UpdateProduct { get; set; }

    int Id { get; set; }

    int RoomNumber { get; set; }

    string RoomType { get; set; }

    bool IsBooked { get; set; }

}
