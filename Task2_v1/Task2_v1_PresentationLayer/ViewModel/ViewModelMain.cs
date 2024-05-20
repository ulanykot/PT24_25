using DataLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2_v1_PresentationLayer.Model;
using Task2_v1_PresentationLayer.ViewModel;

namespace PresentationLayer.ViewModel
{
    internal class ViewModelMain : ViewModelBase
    {
        private IEnumerable<User> _users;
        public IEnumerable<User> Users
        {
            get
            {
                return this._users;
            }
            set
            {
                this._users = value;
                RaisePropertyChanged("Users");
            }
        }
        private IEnumerable<Event> _events;
        public IEnumerable<Event> Events
        {
            get
            {
                return this._events;
            }
            set
            {
                this._events = value;
                RaisePropertyChanged("Events");
            }
        }
        private IEnumerable<State> _states;
        public IEnumerable<State> States
        {
            get
            {
                return this._states;
            }
            set
            {
                this._states = value;
                RaisePropertyChanged("States");
            }
        }
        private IEnumerable<Catalog> _catalog;
        public IEnumerable<Catalog> Catalogs
        {
            get
            {
                return this._catalog;
            }
            set
            {
                this._catalog = value;
                RaisePropertyChanged("Catalog");
            }
        }
        private Event currentEvent;
        public Event CurrentEvent { 
            get 
            { 
                return this.currentEvent; 
            } 
            set
            { 
                this.currentEvent = value; RaisePropertyChanged("CurrentEvent");
                RefreshAll();
            } 
        }

        private CommandBase FetchDataCommend { get; }
        private ModelDataAPI modelData = new ModelData();

        public CommandBase FetchDataCommand { get; }

        //assigning users, events, states, catalogs to be displayed on the screen
        private void RefreshAllUsers()
        {
            this.Users = this.modelData.GetAllUsers();
        }
        private void RefreshAllEvents()
        {
            this.Events = this.modelData.GetAllEvents();
        }
        private void RefreshAllStates()
        {
            this.States = this.modelData.GetAllStates();
        }
        private void RefreshAllCatalogs()
        {
            this.Catalogs = this.modelData.GetAllCatalogs();
        }

        private void RefreshAll()
        {
            this.RefreshAllUsers();
            this.RefreshAllEvents();
            this.RefreshAllStates();
            this.RefreshAllCatalogs();
        }
        public ViewModelMain()
        {
            modelData = ModelDataAPI.Create();
            FetchDataCommand = new CommandBase(() =>
            {
                RefreshAllCatalogs();
                RefreshAllEvents();
                RefreshAllStates();
                RefreshAllUsers();
            });

            FetchDataCommand.Execute(null); // Fetch data initially
        }

    }

}
