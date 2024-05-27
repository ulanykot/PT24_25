using PresentationLayer.Model;
using PresentationLayer.Model.API;
using PresentationLayer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task2_v1_PresentationLayer.ViewModel;

namespace PresentationLayer.ViewModel
{
    internal class DetailState : IViewModel
    {
        public ICommand UpdateState { get; set; }

        private readonly IStateModel _modelOperation;


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

        private int? _catalogId;

        public int? CatalogId
        {
            get => _catalogId;
            set
            {
                _catalogId = value;
                RaisePropertyChanged(nameof(CatalogId));
            }
        }

        private int? _price;

        public int? Price
        {
            get => _price;
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        public DetailState(IStateModel model = null)
        {
            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = ModelFactory.CreateStateModelOperation();
            //this._informer = informer ?? new PopupErrorInformer();
        }

        public DetailState(int id, int? productId, int? productQuantity, IStateModel model = null)
        {
            this.Id = id;
            this.CatalogId = productId;
            this.Price = productQuantity;

            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = ModelFactory.CreateStateModelOperation();
            //this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Id, (int)this.CatalogId, (int)this.Price);

                //this._informer.InformSuccess("State successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Price.ToString()) ||
                this.Price < 0
            );
        }
    }
}
