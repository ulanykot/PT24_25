using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PresentationModel;

namespace PresentationViewModel;

public class EventMasterViewModel : IViewModel, IEventMasterViewModel
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
            IsEventDetailVisible = value ? Visibility.Visible : Visibility.Hidden;
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
            IsEventSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }
    public EventMasterViewModel(IEventModelOperation model = null, IErrorInformer? informer = null)
    {
        SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");

        CheckInEvent = new OnClickCommand(e => HotelCheckInEvent(), c => CanPurchaseEvent());
        CheckOutEvent = new OnClickCommand(e => HotelCheckOutEvent(), c => CanReturnEvent());
        RemoveEvent = new OnClickCommand(e => DeleteEvent());

        Events = new ObservableCollection<IEventDetailViewModel>();

        _modelOperation = IEventModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsEventSelected = false;

        Task.Run(LoadEvents);
    }
    private bool CanPurchaseEvent()
    {
        return !(
            string.IsNullOrWhiteSpace(StateId.ToString()) ||
            string.IsNullOrWhiteSpace(UserId.ToString())
        );
    }

    private bool CanReturnEvent()
    {
        return !(
            string.IsNullOrWhiteSpace(StateId.ToString()) ||
            string.IsNullOrWhiteSpace(UserId.ToString())
        );
    }
    private void HotelCheckInEvent()
    {
        Task.Run(async () =>
        {
            try
            {
                int lastId = await _modelOperation.GetCountAsync() + 1;

                await _modelOperation.AddAsync(lastId, StateId, UserId, CheckIn, CheckOut, "CheckIn");

                LoadEvents();
                _informer.InformSuccess("Event successfully created!");

            }
            catch (Exception e)
            {
                _informer.InformError(e.Message);
            }
        });
    }

    private void HotelCheckOutEvent()
    {
        Task.Run(async () =>
        {
            int lastId = await _modelOperation.GetCountAsync() + 1;

            await _modelOperation.AddAsync(lastId, StateId, UserId, CheckIn, CheckOut, "CheckOut");

            LoadEvents();

            _informer.InformSuccess("Event successfully created!");
        });
    }
    private void DeleteEvent()
    {
        Task.Run(async () =>
        {
            await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

            LoadEvents();

            _informer.InformSuccess("Event successfully deleted!");
        });
    }

    private async void LoadEvents()
    {
        Dictionary<int, IEventModel> Events = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _events.Clear();

            foreach (IEventModel e in Events.Values)
            {
                _events.Add(new EventDetailViewModel(e.Id, e.StateId, e.UserId, e.CheckInDate, e.CheckOutDate, e.Type));
            }
        });

        OnPropertyChanged(nameof(Events));
    }
}
