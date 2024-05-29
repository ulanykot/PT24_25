using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model.API;

namespace PresentationViewModel;

internal class EventMasterViewModel : IViewModel, IEventMasterViewModel
{
    private readonly IErrorInformer _informer;
    public ICommand SwitchToUserMasterPage { get; set; }
    public ICommand SwitchToProductMasterPage { get; set; }
    public ICommand SwitchToStateMasterPage { get; set; }
    public ICommand CheckInEvent { get; set; }
    public ICommand CheckOutEvent { get; set; }
    public ICommand RemoveEvent { get; set; }

    private readonly IEventModelOperation _modelOperation;

    private ObservableCollection<IEventDetailViewModel> _events;
    public ObservableCollection<IEventDetailViewModel> Events
    {
        get => _events;
        set
        {
            _events = value;
            OnPropertyChanged(nameof(Events));
        }
    }
    private int _stateId;
    public int StateId
    {
        get => _stateId;
        set
        {
            _stateId = value;
            OnPropertyChanged(nameof(StateId));
        }
    }
    private DateTime _checkIn;
    public DateTime CheckIn
    {
        get => _checkIn;
        set
        {
            _checkIn = value;
            OnPropertyChanged(nameof(CheckIn));
        }
    }
    private DateTime _checkOut;
    public DateTime CheckOut
    {
        get => _checkOut;
        set
        {
            _checkOut = value;
            OnPropertyChanged(nameof(CheckOut));
        }
    }
    private int _userId;
    public int UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            OnPropertyChanged(nameof(UserId));
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
            OnPropertyChanged(nameof(IsEventSelected));
        }
    }
    private Visibility _isEventDetailVisible;
    public Visibility IsEventDetailVisible
    {
        get => _isEventDetailVisible;
        set
        {
            _isEventDetailVisible = value;
            OnPropertyChanged(nameof(IsEventDetailVisible));
        }
    }
    private IEventDetailViewModel _selectedDetailViewModel;

    public IEventDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            this.IsEventSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }
    public EventMasterViewModel(IEventModelOperation model = null, IErrorInformer? informer = null)
    {
        this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        this.SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        this.SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");

        this.CheckInEvent = new OnClickCommand(e => this.HotelCheckInEvent(), c => this.CanPurchaseEvent());
        this.CheckOutEvent = new OnClickCommand(e => this.HotelCheckOutEvent(), c => this.CanReturnEvent());
        this.RemoveEvent = new OnClickCommand(e => this.DeleteEvent());

        this.Events = new ObservableCollection<IEventDetailViewModel>();

        this._modelOperation = IEventModelOperation.CreateModelOperation();
        this._informer = informer ?? new PopupErrorInformer();

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
                this._informer.InformSuccess("Event successfully created!");

            }
            catch (Exception e)
            {
                this._informer.InformError(e.Message);
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

            this._informer.InformSuccess("Event successfully created!");
        });
    }
    private void DeleteEvent()
    {
        Task.Run(async () =>
        {
            await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Id);

            this.LoadEvents();

            this._informer.InformSuccess("Event successfully deleted!");
        });
    }

    private async void LoadEvents()
    {
        Dictionary<int, IEventModel> Events = await this._modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            this._events.Clear();

            foreach (IEventModel e in Events.Values)
            {
                this._events.Add(new EventDetailViewModel(e.Id, (int)e.StateId, (int)e.UserId, (DateTime)e.CheckInDate, (DateTime)e.CheckOutDate, e.Type));
            }
        });

        OnPropertyChanged(nameof(Events));
    }
}
