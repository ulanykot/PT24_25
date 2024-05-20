using DataLayer;
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
    internal class ViewModel : ViewModelBase
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

        private CommandBase FetchDataCommend { get; }
        private ModelDataAPI modelData { get; set; }

        public ViewModel(ModelDataAPI dataLayer)
        {
            FetchDataCommend = new CommandBase(() => modelData = dataLayer ?? ModelDataAPI.Create());
        }


    }
}
