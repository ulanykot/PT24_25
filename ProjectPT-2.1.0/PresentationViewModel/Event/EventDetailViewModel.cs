using System;
using System.Threading.Tasks;
using System.Windows.Input;

using PresentationModel;

namespace PresentationViewModel;

public class EventDetailViewModel : IViewModel, IEventDetailViewModel
{
    public ICommand UpdateEvent { get; set; }

    private readonly IEventModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private int _id;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
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

    private string _type;

    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    public EventDetailViewModel(IEventModelOperation? model = null, IErrorInformer? informer = null)
    {
        UpdateEvent = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = IEventModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    public EventDetailViewModel(int id, int stateId, int userId, DateTime checkOut, DateTime checkIn, string type, IEventModelOperation? model = null, IErrorInformer? informer = null)
    {
        UpdateEvent = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = IEventModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        Id = id;
        StateId = stateId;
        UserId = userId;
        CheckOut = checkOut;
        CheckIn = checkIn;
        Type = type;
    }

    private void Update()
    {
        Task.Run(async () =>
        {
            await _modelOperation.UpdateAsync(Id, StateId, UserId, CheckIn, CheckOut, Type);

            _informer.InformSuccess("Event successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
             string.IsNullOrWhiteSpace(CheckIn.ToString()) ||
                 string.IsNullOrWhiteSpace(CheckOut.ToString())
        );
    }
}
