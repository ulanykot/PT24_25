using System;
using System.Collections.ObjectModel;
using DataLayer;
using Task2_v1_PresentationLayer.Model;
using Task2_v1_PresentationLayer.ViewModel;

namespace PresentationLayer.ViewModel
{
    internal class ViewModelMain : ViewModelBase
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get => _events;
            set
            {
                _events = value;
                RaisePropertyChanged(nameof(Events));
            }
        }

        private ObservableCollection<State> _states;
        public ObservableCollection<State> States
        {
            get => _states;
            set
            {
                _states = value;
                RaisePropertyChanged(nameof(States));
            }
        }

        private ObservableCollection<Catalog> _catalogs;
        public ObservableCollection<Catalog> Catalogs
        {
            get => _catalogs;
            set
            {
                _catalogs = value;
                RaisePropertyChanged(nameof(Catalogs));
            }
        }

        private CommandBase FetchDataCommand { get; }
        private ModelDataAPI modelData;

        public ViewModelMain(ModelDataAPI dataLayer)
        {
            modelData = dataLayer ?? ModelDataAPI.Create();

            // Initialize collections
            Users = new ObservableCollection<User>(modelData.GetAllUsers());
            Events = new ObservableCollection<Event>(modelData.GetAllEvents());
            States = new ObservableCollection<State>(modelData.GetAllStates());
            Catalogs = new ObservableCollection<Catalog>(modelData.GetAllCatalogs());

            // Subscribe to model data changes
            modelData.DataChanged += OnDataChanged;
        }

        public ViewModelMain() : this(null) { }

        private void OnDataChanged(object sender, EventArgs e)
        {
            RefreshAllData();
        }

        private void RefreshAllData()
        {
            Users = new ObservableCollection<User>(modelData.GetAllUsers());
            Events = new ObservableCollection<Event>(modelData.GetAllEvents());
            States = new ObservableCollection<State>(modelData.GetAllStates());
            Catalogs = new ObservableCollection<Catalog>(modelData.GetAllCatalogs());
        }
    }
}
