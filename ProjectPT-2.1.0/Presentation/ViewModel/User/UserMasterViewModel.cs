using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model.API;

namespace PresentationViewModel;

internal class UserMasterViewModel : IViewModel, IUserMasterViewModel
{
    public ICommand SwitchToProductMasterPage { get; set; }

    public ICommand SwitchToStateMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateUser { get; set; }

    public ICommand RemoveUser { get; set; }

    private readonly IUserModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<IUserDetailViewModel> _users;

    public ObservableCollection<IUserDetailViewModel> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    private string _name;

    public string FirstName
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _email;

    public string LastName
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
    private string _userType;
    public string UserType
    {
        get => _userType;
        set
        {
            _userType = value;
            OnPropertyChanged(nameof(UserType));
        }
    }

    private bool _isUserSelected;

    public bool IsUserSelected
    {
        get => _isUserSelected;
        set
        {
            IsUserDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isUserSelected = value;
            OnPropertyChanged(nameof(IsUserSelected));
        }
    }

    private Visibility _isUserDetailVisible;

    public Visibility IsUserDetailVisible
    {
        get => _isUserDetailVisible;
        set
        {
            _isUserDetailVisible = value;
            OnPropertyChanged(nameof(IsUserDetailVisible));
        }
    }

    private IUserDetailViewModel _selectedDetailViewModel;

    public IUserDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsUserSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public UserMasterViewModel(IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateUser = new OnClickCommand(e => StoreUser(), c => CanStoreUser());
        RemoveUser = new OnClickCommand(e => DeleteUser());

        Users = new ObservableCollection<IUserDetailViewModel>();

        _modelOperation = model ?? IUserModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsUserSelected = false;

        Task.Run(LoadUsers);
    }

    private bool CanStoreUser()
    {
        return !(
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName)
        );
    }

    private void StoreUser()
    {
        Task.Run(async () =>
        {
            int lastId = await _modelOperation.GetCountAsync() + 1;

            await _modelOperation.AddAsync(lastId, FirstName, LastName, UserType);

            _informer.InformSuccess("User successfully created!");

            LoadUsers();
        });
    }

    private void DeleteUser()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                _informer.InformSuccess("User successfully deleted!");

                LoadUsers();
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting user! Remember to remove all associated events!");
            }
        });
    }

    private async void LoadUsers()
    {
        Dictionary<int, IUserModel> Users = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _users.Clear();

            foreach (IUserModel u in Users.Values)
            {
                _users.Add(new UserDetailViewModel(u.Id, u.FirstName, u.LastName, u.UserType));
            }
        });

        OnPropertyChanged(nameof(Users));
    }
}
