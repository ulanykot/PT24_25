using PresentationLayer.Model;
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
using PresentationLayer.Model.API;
using ServiceLayer.API;

namespace PresentationLayer.ViewModel
{
    internal class MasterState : IViewModel
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateState { get; set; }

        public ICommand RemoveState { get; set; }

        private readonly IStateModel _modelOperation;

        private ObservableCollection<DetailState> _states;

        public ObservableCollection<DetailState> States
        {
            get => _states;
            set
            {
                _states = value;
                RaisePropertyChanged(nameof(States));
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

        private int _catalogId;

        public int CatalogId
        {
            get => _catalogId;
            set
            {
                _catalogId = value;
                RaisePropertyChanged(nameof(CatalogId));
            }
        }

        private int _price;

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        private bool _isStateSelected;

        public bool IsStateSelected
        {
            get => _isStateSelected;
            set
            {
                this.IsStateDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isStateSelected = value;
                RaisePropertyChanged(nameof(IsStateSelected));
            }
        }

        private Visibility _isStateDetailVisible;

        public Visibility IsStateDetailVisible
        {
            get => _isStateDetailVisible;
            set
            {
                _isStateDetailVisible = value;
                RaisePropertyChanged(nameof(IsStateDetailVisible));
            }
        }

        private DetailState _selectedDetailViewModel;

        public DetailState SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsStateSelected = true;

                RaisePropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public MasterState(IStateModel model = null)
        {
            this.SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
            this.SwitchToProductMasterPage = new SwitchViewCommand("ProductMasterView");
            this.SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

            this.CreateState = new OnClickCommand(e => this.StoreState(), c => this.CanStoreState());
            this.RemoveState = new OnClickCommand(e => this.DeleteState());

            this.States = new ObservableCollection<DetailState>();

            this._modelOperation = ModelFactory.CreateStateModelOperation();
            ////this._informer = informer ?? new PopupErrorInformer();

            this.IsStateSelected = false;

            Task.Run(this.LoadStates);
        }

        private bool CanStoreState()
        {
            return !(
                string.IsNullOrWhiteSpace(this.CatalogId.ToString()) ||
                string.IsNullOrWhiteSpace(this.Price.ToString()) ||
                this.Price < 0
            );
        }

        private void StoreState()
        {
            Task.Run(async () =>
            {
                try
                {
                    int lastId = await this._modelOperation.GetCountAsync() + 1;

                    await this._modelOperation.AddAsync(lastId, this.CatalogId, this.Price);

                    this.LoadStates();

                    //this._informer.InformSuccess("State successfully created!");
                }
                catch (Exception e)
                {
                    //this._informer.InformError(e.Message);
                }
            });
        }

        private void DeleteState()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelOperation.DeleteAsync(this.SelectedDetailViewModel.Id);

                    this.LoadStates();

                    //this._informer.InformSuccess("State successfully deleted!");
                }
                catch (Exception e)
                {
                    //this._informer.InformError("Error while deleting state! Remember to remove all associated events!");
                }
            });
        }

        private async void LoadStates()
        {
            Dictionary<int, IStateService> States = await this._modelOperation.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._states.Clear();

                foreach (IStateService s in States.Values)
                {
                    this._states.Add(new DetailState(s.Id, s.RoomCatalogId, s.Price));
                }
            });

            RaisePropertyChanged(nameof(States));
        }
    }
}
