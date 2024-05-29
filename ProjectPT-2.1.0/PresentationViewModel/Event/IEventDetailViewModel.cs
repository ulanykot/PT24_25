using PresentationModel;
using System;
using System.Windows.Input;

namespace PresentationViewModel;

public interface IEventDetailViewModel
{
    static IEventDetailViewModel CreateViewModel(int id, int stateId, int userId, DateTime checkIn, DateTime checkOut,
        string type, IEventModelOperation model, IErrorInformer informer)
    {
        return new EventDetailViewModel(id, stateId, userId, checkIn, checkOut, type, model, informer);
    }

    ICommand UpdateEvent { get; set; }

    int Id { get; set; }

    int StateId { get; set; }

    int UserId { get; set; }

    DateTime CheckIn { get; set; }

    DateTime CheckOut { get; set; }

    string Type { get; set; }
}
