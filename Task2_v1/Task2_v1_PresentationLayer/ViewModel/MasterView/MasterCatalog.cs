using PresentationLayer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Task2_v1_PresentationLayer.ViewModel;
using PresentationLayer.ViewModel.DetailView;
using PresentationLayer.Model;
using PresentationLayer.Model.API;
using ServiceLayer.API;

namespace PresentationLayer.ViewModel.MasterView
{
    internal class MasterCatalog : IViewModel
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToStateMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateProduct { get; set; }

        public ICommand RemoveProduct { get; set; }

        private readonly ICatalogModel _modelOperation;

        private ObservableCollection<DetailCatalog> _catalogs;

        public ObservableCollection<DetailCatalog> Catalogs
        {
            get => _catalogs;
            set
            {
                _catalogs = value;
                RaisePropertyChanged(nameof(Catalogs));
            }
        }

        private int _roomNumber;

        public int RoomNumber
        {
            get => _roomNumber;
            set
            {
                _roomNumber = value;
                RaisePropertyChanged(nameof(RoomNumber));
            }
        }

        private string _roomType;

        public string RoomType
        {
            get => _roomType;
            set
            {
                _roomType = value;
                RaisePropertyChanged(nameof(RoomType));
            }
        }
        private bool _isBooked;

        public bool IsBooked
        {
            get => _isBooked;
            set
            {
                _isBooked = value;
                RaisePropertyChanged(nameof(IsBooked));
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
                RaisePropertyChanged(nameof(IsProductSelected));
            }
        }

        private Visibility _isProductDetailVisible;

        public Visibility IsProductDetailVisible
        {
            get => _isProductDetailVisible;
            set
            {
                _isProductDetailVisible = value;
                RaisePropertyChanged(nameof(IsProductDetailVisible));
            }
        }

        private DetailCatalog _selectedDetailViewModel;

        public DetailCatalog SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                IsProductSelected = true;

                RaisePropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public MasterCatalog()
        {
            SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            CreateProduct = new OnClickCommand(e => StoreProduct(), c => CanStoreProduct());
            RemoveProduct = new OnClickCommand(e => DeleteProduct());

            Catalogs = new ObservableCollection<DetailCatalog>();

            _modelOperation = ModelFactory.CreateCatalogModelOpetation();
            //_informer = informer ?? new PopupErrorInformer();

            IsProductSelected = false;

            Task.Run(this.LoadProducts);
        }

        public MasterCatalog(ICatalogModel model = null)
        {
            SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
            SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            CreateProduct = new OnClickCommand(e => StoreProduct(), c => CanStoreProduct());
            RemoveProduct = new OnClickCommand(e => DeleteProduct());

            Catalogs = new ObservableCollection<DetailCatalog>();

            _modelOperation = model ?? ModelFactory.CreateCatalogModelOpetation();
            //_informer = informer ?? new PopupErrorInformer();

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

                //_informer.InformSuccess("Product added successfully!");

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

                    //_informer.InformSuccess("Product deleted successfully!");
                }
                catch (Exception e)
                {
                    //_informer.InformError("Error while deleting product! Remember to remove all associated states!");
                }
            });
        }

        private async void LoadProducts()
        {
            Dictionary<int, ICatalogService> Products = await _modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                _catalogs.Clear();

                foreach (ICatalogService p in Products.Values)
                {
                    _catalogs.Add(new DetailCatalog(p.Id, (int)p.RoomNumber, p.RoomType, (bool)p.isBooked));
                }
            });

            RaisePropertyChanged(nameof(Products));
        }
    }
}
