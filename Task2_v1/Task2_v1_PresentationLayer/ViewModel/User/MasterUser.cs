using PresentationLayer.Model;
using PresentationLayer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Task2_v1_PresentationLayer.ViewModel;
using PresentationLayer.Model.API;
using ServiceLayer.API;

namespace PresentationLayer.ViewModel
{
    internal class MasterUser : IViewModel
    {

        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToStateMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateUser { get; set; }

        public ICommand RemoveUser { get; set; }

        private readonly IUserModel _modelOperation;

        private ObservableCollection<DetailUser> _users;

        public ObservableCollection<DetailUser> Users
        {
            get => _users;
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }
        private string _userType;
        public string UserType
        {
            get => _userType;
            set
            {
                _userType = value;
                RaisePropertyChanged(nameof(UserType));
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
                RaisePropertyChanged(nameof(IsUserSelected));
            }
        }

        private Visibility _isUserDetailVisible;

        public Visibility IsUserDetailVisible
        {
            get => _isUserDetailVisible;
            set
            {
                _isUserDetailVisible = value;
                RaisePropertyChanged(nameof(IsUserDetailVisible));
            }
        }

        private DetailUser _selectedDetailViewModel;

        public DetailUser SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                IsUserSelected = true;

                RaisePropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public MasterUser(IUserModel model = null)
        {
            SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
            SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            CreateUser = new OnClickCommand(e => StoreUser(), c => CanStoreUser());
            RemoveUser = new OnClickCommand(e => DeleteUser());

            Users = new ObservableCollection<DetailUser>();

            _modelOperation = model ?? ModelFactory.CreateUserModelOperation();
            //_informer = informer ?? new PopupErrorInformer();

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

                //_informer.InformSuccess("User successfully created!");

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

                    //_informer.InformSuccess("User successfully deleted!");

                    LoadUsers();
                }
                catch (Exception e)
                {
                    //_informer.InformError("Error while deleting user! Remember to remove all associated events!");
                }
            });
        }

        private async void LoadUsers()
        {
            Dictionary<int, IUserService> Users = await _modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _users.Clear();

                foreach (IUserService u in Users.Values)
                {
                    _users.Add(new DetailUser(u.Id, u.FirstName, u.LastName, u.UserType));
                }
            });

            RaisePropertyChanged(nameof(Users));
        }
    }
}
