using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class CatalogMasterViewModel : IViewModel, ICatalogMasterViewModel
{
    public ICommand SwitchToUserMasterPage { get; set; }

    public ICommand SwitchToStateMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateCatalog { get; set; }

    public ICommand RemoveCatalog { get; set; }

    private readonly ICatalogModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<ICatalogDetailViewModel> _catalogs;

    public ObservableCollection<ICatalogDetailViewModel> Catalogs
    {
        get => _catalogs;
        set
        {
            _catalogs = value;
            OnPropertyChanged(nameof(Catalogs));
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

    private bool _isCatalogSelected;

    public bool IsCatalogSelected
    {
        get => _isCatalogSelected;
        set
        {
            IsCatalogDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isCatalogSelected = value;
            OnPropertyChanged(nameof(IsCatalogSelected));
        }
    }

    private Visibility _isCatalogDetailVisible;

    public Visibility IsCatalogDetailVisible
    {
        get => _isCatalogDetailVisible;
        set
        {
            _isCatalogDetailVisible = value;
            OnPropertyChanged(nameof(IsCatalogDetailVisible));
        }
    }

    private ICatalogDetailViewModel _selectedDetailViewModel;

    public ICatalogDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsCatalogSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public CatalogMasterViewModel(ICatalogModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateCatalog = new OnClickCommand(e => StoreCatalog(), c => CanStoreCatalog());
        RemoveCatalog = new OnClickCommand(e => DeleteCatalog());

        Catalogs = new ObservableCollection<ICatalogDetailViewModel>();

        _modelOperation = model ?? ICatalogModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsCatalogSelected = false;

        Task.Run(this.LoadCatalogs);
    }

    private bool CanStoreCatalog()
    {
        return !(
            string.IsNullOrWhiteSpace(this.RoomType)
        );
    }

    private void StoreCatalog()
    {
        Task.Run(async () =>
        {
            int lastId = await this._modelOperation.GetCountAsync() + 1;

            await this._modelOperation.AddAsync(lastId, RoomNumber, RoomType, IsBooked);

            LoadCatalogs();

            _informer.InformSuccess("Catalog added successfully!");

        });
    }

    private void DeleteCatalog()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                LoadCatalogs();

                _informer.InformSuccess("Catalog deleted successfully!");
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting catalog! Remember to remove all associated states!");
            }
        });
    }

    private async void LoadCatalogs()
    {
        Dictionary<int, ICatalogModel> Catalogs = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _catalogs.Clear();

            foreach (ICatalogModel p in Catalogs.Values)
            {
                _catalogs.Add(new CatalogDetailViewModel(p.Id, (int)p.RoomNumber, p.RoomType, (bool)p.isBooked));
            }
        });

        OnPropertyChanged(nameof(Catalogs));
    }
}
