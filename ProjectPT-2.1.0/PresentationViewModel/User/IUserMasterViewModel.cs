using PresentationModel;
using PresentationViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PresentationViewModel;

public interface IUserMasterViewModel
{
    static IUserMasterViewModel CreateViewModel(IUserModelOperation operation, IErrorInformer informer)
    {
        return new UserMasterViewModel(operation, informer);
    }

    ICommand CreateUser { get; set; }

    ICommand RemoveUser { get; set; }

    ObservableCollection<IUserDetailViewModel> Users { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string UserType { get; set; }

    bool IsUserSelected { get; set; }

    Visibility IsUserDetailVisible { get; set; }

    IUserDetailViewModel SelectedDetailViewModel { get; set; }
}
