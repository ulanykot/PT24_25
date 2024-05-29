
using PresentationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationViewModel;

public class StateMasterViewModel : IViewModel, IStateMasterViewModel
{
    public ICommand SwitchToUserMasterPage { get; set; }

    public ICommand SwitchToProductMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateState { get; set; }

    public ICommand RemoveState { get; set; }

    private readonly IStateModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<IStateDetailViewModel> _states;

    public ObservableCollection<IStateDetailViewModel> States
    {
        get => _states;
        set
        {
            _states = value;
            OnPropertyChanged(nameof(States));
        }
    }

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

    private bool _isStateSelected;

    public bool IsStateSelected
    {
        get => _isStateSelected;
        set
        {
            IsStateDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isStateSelected = value;
            OnPropertyChanged(nameof(IsStateSelected));
        }
    }

    private Visibility _isStateDetailVisible;

    public Visibility IsStateDetailVisible
    {
        get => _isStateDetailVisible;
        set
        {
            _isStateDetailVisible = value;
            OnPropertyChanged(nameof(IsStateDetailVisible));
        }
    }

    private IStateDetailViewModel _selectedDetailViewModel;

    public IStateDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsStateSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public StateMasterViewModel(IStateModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateState = new OnClickCommand(e => StoreState(), c => CanStoreState());
        RemoveState = new OnClickCommand(e => DeleteState());

        States = new ObservableCollection<IStateDetailViewModel>();

        _modelOperation = IStateModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsStateSelected = false;

        Task.Run(LoadStates);
    }

    private bool CanStoreState()
    {
        return !(
            string.IsNullOrWhiteSpace(ProductId.ToString()) ||
            string.IsNullOrWhiteSpace(Price.ToString()) ||
            Price < 0
        );
    }

    private void StoreState()
    {
        Task.Run(async () =>
        {
            try
            {
                int lastId = await _modelOperation.GetCountAsync() + 1;

                await _modelOperation.AddAsync(lastId, ProductId, Price);

                LoadStates();

                _informer.InformSuccess("State successfully created!");
            }
            catch (Exception e)
            {
                _informer.InformError(e.Message);
            }
        });
    }

    private void DeleteState()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                LoadStates();

                _informer.InformSuccess("State successfully deleted!");
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting state! Remember to remove all associated events!");
            }
        });
    }

    private async void LoadStates()
    {
        Dictionary<int, IStateModel> States = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _states.Clear();

            foreach (IStateModel s in States.Values)
            {
                _states.Add(new StateDetailViewModel(s.Id, s.RoomCatalogId, s.Price));
            }
        });

        OnPropertyChanged(nameof(States));
    }
}
