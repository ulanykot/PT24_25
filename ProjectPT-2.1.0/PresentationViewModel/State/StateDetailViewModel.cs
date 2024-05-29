using System.Threading.Tasks;
using System.Windows.Input;

using PresentationModel;

namespace PresentationViewModel;

public class StateDetailViewModel : IViewModel, IStateDetailViewModel
{
    public ICommand UpdateState { get; set; }

    private readonly IStateModelOperation _modelOperation;

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

    private int _productId;

    public int ProductId
    {
        get => _productId;
        set
        {
            _productId = value;
            OnPropertyChanged(nameof(ProductId));
        }
    }

    private int _price;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged(nameof(Price));
        }
    }

    public StateDetailViewModel(IStateModelOperation? model = null, IErrorInformer? informer = null)
    {
        UpdateState = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = IStateModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    public StateDetailViewModel(int id, int productId, int productQuantity, IStateModelOperation? model = null, IErrorInformer? informer = null)
    {
        Id = id;
        ProductId = productId;
        Price = productQuantity;

        UpdateState = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = IStateModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            _modelOperation.UpdateAsync(Id, ProductId, Price);

            _informer.InformSuccess("State successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(Price.ToString()) ||
            Price < 0
        );
    }
}
