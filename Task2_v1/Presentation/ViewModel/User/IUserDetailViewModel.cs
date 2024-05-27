using Presentation.Model.API;
using System;
using System.Windows.Input;

namespace Presentation.ViewModel;

public interface IUserDetailViewModel
{
    static IUserDetailViewModel CreateViewModel(int id, string firstName, string lastName, string userType, IUserModelOperation model, IErrorInformer informer)
    {
        return new UserDetailViewModel(id, firstName, lastName, userType, model, informer);
    }

    ICommand UpdateUser { get; set; }

    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }
    string UserType { get; set; }
}

