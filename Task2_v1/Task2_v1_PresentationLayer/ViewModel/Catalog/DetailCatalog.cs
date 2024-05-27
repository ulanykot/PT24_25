using PresentationLayer.Model;
using PresentationLayer.Model.API;
using PresentationLayer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Task2_v1_PresentationLayer.ViewModel;

namespace PresentationLayer.ViewModel
{
    public class DetailCatalog : IViewModel
    {
        public ICommand UpdateProduct { get; set; }

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
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
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

        public DetailCatalog()
        {
            UpdateProduct = new OnClickCommand(e => Update(), c => CanUpdate());

            _modelOperation = ModelFactory.CreateCatalogModelOpetation();
            //_informer = informer ?? new PopupErrorInformer();
        }
        public DetailCatalog(ICatalogModel model = null)
        {
            UpdateProduct = new OnClickCommand(e => Update(), c => CanUpdate());

            _modelOperation = model ?? ModelFactory.CreateCatalogModelOpetation();
            //_informer = informer ?? new PopupErrorInformer();
        }

        public DetailCatalog(int id, int roomNo, string RoomType, bool isBooked, ICatalogModel model = null)
        {
            IsBooked = isBooked;
            this.Id = id;
            this.RoomNumber = roomNo;
            this.RoomType = RoomType;


            UpdateProduct = new OnClickCommand(e => Update(), c => CanUpdate());

            _modelOperation = model ?? ModelFactory.CreateCatalogModelOpetation();
            //_informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                _modelOperation.UpdateAsync(Id, RoomNumber, RoomType, IsBooked);

                //_informer.InformSuccess("Product successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(RoomType)
                        );
        }

    }
}
