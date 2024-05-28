using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class CatalogDetailViewModel : IViewModel, ICatalogDetailViewModel
{
    public ICommand UpdateCatalog { get; set; }

    private readonly ICatalogModelOperation _modelOperation;

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


    private int _roomNumber;

    public int RoomNumber
    {
        get => _roomNumber;
        set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    private string _roomType;

    public string RoomType
    {
        get => _roomType;
        set
        {
            _roomType = value;
            OnPropertyChanged(nameof(RoomType));
        }
    }
    private bool _isBooked;

    public bool IsBooked
    {
        get => _isBooked;
        set
        {
            _isBooked = value;
            OnPropertyChanged(nameof(IsBooked));
        }
    }

    public CatalogDetailViewModel(ICatalogModelOperation? model = null, IErrorInformer? informer = null)
    {
        UpdateCatalog = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? ICatalogModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    public CatalogDetailViewModel(int id, int roomNumber, string roomType, bool isBooked, ICatalogModelOperation? model = null, IErrorInformer? informer = null)
    {
        Id = id;
        RoomNumber = roomNumber;
        RoomType = roomType;
        IsBooked = isBooked;

        UpdateCatalog = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? ICatalogModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            _modelOperation.UpdateAsync(Id, RoomNumber, RoomType, IsBooked);

            _informer.InformSuccess("Catalog successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(RoomType)
                    );
    }
}
