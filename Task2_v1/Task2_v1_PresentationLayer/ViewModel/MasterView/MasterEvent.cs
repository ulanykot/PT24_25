using PresentationLayer.Model;
using PresentationLayer.Model.API;
using PresentationLayer.ViewModel.Commands;
using PresentationLayer.ViewModel.DetailView;
using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task2_v1_PresentationLayer.ViewModel;

namespace PresentationLayer.ViewModel.MasterView
{
    internal class MasterEvent : IViewModel
    {
        public ICommand SwitchToUserMasterPage { get; set; }
        public ICommand SwitchToProductMasterPage { get; set; }
        public ICommand SwitchToStateMasterPage { get; set; }
        public ICommand CheckInEvent { get; set; }
        public ICommand CheckOutEvent { get; set; }
        public ICommand RemoveEvent { get; set; }
        private readonly IEventModel _modelOperation;
        private ObservableCollection<DetailEvent> _events;
        public ObservableCollection<DetailEvent> Events
        {
            get => _events;
            set
            {
                _events = value;
                RaisePropertyChanged(nameof(Events));
            }
        }
        private int _stateId;
        public int StateId
        {
            get => _stateId;
            set
            {
                _stateId = value;
                RaisePropertyChanged(nameof(StateId));
            }
        }
        private DateTime _checkIn;
        public DateTime CheckIn
        {
            get => _checkIn;
            set
            {
                _checkIn = value;
                RaisePropertyChanged(nameof(CheckIn));
            }
        }
        private DateTime _checkOut;
        public DateTime CheckOut
        {
            get => _checkOut;
            set
            {
                _checkOut = value;
                RaisePropertyChanged(nameof(CheckOut));
            }
        }
        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                RaisePropertyChanged(nameof(UserId));
            }
        }
        private bool _isEventSelected;
        public bool IsEventSelected
        {
            get => _isEventSelected;
            set
            {
                this.IsEventDetailVisible = value ? Visibility.Visible : Visibility.Hidden;
                _isEventSelected = value;
                RaisePropertyChanged(nameof(IsEventSelected));
            }
        }
        private Visibility _isEventDetailVisible;
        public Visibility IsEventDetailVisible
        {
            get => _isEventDetailVisible;
            set
            {
                _isEventDetailVisible = value;
                RaisePropertyChanged(nameof(IsEventDetailVisible));
            }
        }
        private DetailEvent _selectedDetailViewModel;

        public DetailEvent SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsEventSelected = true;

                RaisePropertyChanged(nameof(SelectedDetailViewModel));
            }
        }
        public MasterEvent(IEventModel model = null)
        {
            this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            this.SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            this.SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");

            this.CheckInEvent = new OnClickCommand(e => this.HotelCheckInEvent(), c => this.CanPurchaseEvent());
            this.CheckOutEvent = new OnClickCommand(e => this.HotelCheckOutEvent(), c => this.CanReturnEvent());            
            this.RemoveEvent = new OnClickCommand(e => this.DeleteEvent());

            this.Events = new ObservableCollection<DetailEvent>();

            this._modelOperation = ModelFactory.CreateEventModelOperation();

            this.IsEventSelected = false;

            Task.Run(this.LoadEvents);
        }
        private bool CanPurchaseEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(this.StateId.ToString()) ||
                string.IsNullOrWhiteSpace(this.UserId.ToString())
            );
        }

        private bool CanReturnEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(this.StateId.ToString()) ||
                string.IsNullOrWhiteSpace(this.UserId.ToString())
            );
        }
        private void HotelCheckInEvent()
        {
            Task.Run(async () =>
            {
                try
                {
                    int lastId = await this._modelOperation.GetCountAsync() + 1;

                    await this._modelOperation.AddAsync(lastId, this.StateId, this.UserId, this.CheckIn, this.CheckOut, "CheckIn");

                    this.LoadEvents();
                    //this._informer.InformSuccess("Event successfully created!");

                }
                catch (Exception e)
                {
                    //  this._informer.InformError(e.Message);
                }
            });
        }

        private void HotelCheckOutEvent()
        {
            Task.Run(async () =>
            {
                int lastId = await this._modelOperation.GetCountAsync() + 1;

                await this._modelOperation.AddAsync(lastId, this.StateId, this.UserId, this.CheckIn, this.CheckOut, "CheckOut");

                this.LoadEvents();

                //this._informer.InformSuccess("Event successfully created!");
            });
        }
        private void DeleteEvent()
        {
            Task.Run(async () =>
            {
                await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Id);

                this.LoadEvents();

                //this._informer.InformSuccess("Event successfully deleted!");
            });
        }

        private async void LoadEvents()
        {
            Dictionary<int, IEventService> Events = await this._modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._events.Clear();

                foreach (IEventService e in Events.Values)
                {
                    this._events.Add(new DetailEvent(e.Id, e.StateId, e.UserId, e.CheckInDate, e.CheckOutDate, e.Type));
                }
            });

            RaisePropertyChanged(nameof(Events));
        }
    }
}
