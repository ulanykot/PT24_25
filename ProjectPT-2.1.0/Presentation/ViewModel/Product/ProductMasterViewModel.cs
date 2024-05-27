using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class ProductMasterViewModel : IViewModel, IProductMasterViewModel
{
    public ICommand SwitchToUserMasterPage { get; set; }

    public ICommand SwitchToStateMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateProduct { get; set; }

    public ICommand RemoveProduct { get; set; }

    private readonly ICatalogModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<IProductDetailViewModel> _products;

    public ObservableCollection<IProductDetailViewModel> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChanged(nameof(Products));
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

    private bool _isProductSelected;

    public bool IsProductSelected
    {
        get => _isProductSelected;
        set
        {
            IsProductDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isProductSelected = value;
            OnPropertyChanged(nameof(IsProductSelected));
        }
    }

    private Visibility _isProductDetailVisible;

    public Visibility IsProductDetailVisible
    {
        get => _isProductDetailVisible;
        set
        {
            _isProductDetailVisible = value;
            OnPropertyChanged(nameof(IsProductDetailVisible));
        }
    }

    private IProductDetailViewModel _selectedDetailViewModel;

    public IProductDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsProductSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public ProductMasterViewModel(ICatalogModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateProduct = new OnClickCommand(e => StoreProduct(), c => CanStoreProduct());
        RemoveProduct = new OnClickCommand(e => DeleteProduct());

        Products = new ObservableCollection<IProductDetailViewModel>();

        _modelOperation = model ?? ICatalogModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsProductSelected = false;

        Task.Run(this.LoadProducts);
    }

    private bool CanStoreProduct()
    {
        return !(
            string.IsNullOrWhiteSpace(this.RoomType)
        );
    }

    private void StoreProduct()
    {
        Task.Run(async () =>
        {
            int lastId = await this._modelOperation.GetCountAsync() + 1;

            await this._modelOperation.AddAsync(lastId, RoomNumber, RoomType, IsBooked);

            LoadProducts();

            _informer.InformSuccess("Product added successfully!");

        });
    }

    private void DeleteProduct()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                LoadProducts();

                _informer.InformSuccess("Product deleted successfully!");
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting product! Remember to remove all associated states!");
            }
        });
    }

    private async void LoadProducts()
    {
        Dictionary<int, ICatalogModel> Products = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _products.Clear();

            foreach (ICatalogModel p in Products.Values)
            {
                _products.Add(new ProductDetailViewModel(p.Id, (int)p.RoomNumber, p.RoomType, (bool)p.isBooked));
            }
        });

        OnPropertyChanged(nameof(Products));
    }
}
