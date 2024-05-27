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
    internal class DetailEvent : IViewModel
    {
        public ICommand UpdateEvent { get; set; }

        private readonly IEventModel _modelOperation;

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

        private int? _stateId;

        public int? StateId
        {
            get => _stateId;
            set
            {
                _stateId = value;
                RaisePropertyChanged(nameof(StateId));
            }
        }

        private int? _userId;

        public int? UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                RaisePropertyChanged(nameof(UserId));
            }
        }

        private DateTime? _checkIn;
        public DateTime? CheckIn
        {
            get => _checkIn;
            set
            {
                _checkIn = value;
                RaisePropertyChanged(nameof(CheckIn));
            }
        }
        private DateTime? _checkOut;
        public DateTime? CheckOut
        {
            get => _checkOut;
            set
            {
                _checkOut = value;
                RaisePropertyChanged(nameof(CheckOut));
            }
        }

        private string _type;

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }

        public DetailEvent(IEventModel model = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = ModelFactory.CreateEventModelOperation();
            //this._informer = informer ?? new PopupErrorInformer();
        }

        public DetailEvent(int id, int? stateId, int? userId, DateTime? checkOut, DateTime? checkIn, string type, IEventModel model = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = ModelFactory.CreateEventModelOperation();
            //this._informer = informer ?? new PopupErrorInformer();

            this.Id = id;
            this.StateId = stateId;
            this.UserId = userId;
            this.CheckOut = checkOut;
            this.CheckIn = checkIn;
            this.Type = type;
        }

        private void Update()
        {
            Task.Run(async () =>
            {
                await this._modelOperation.UpdateAsync(this.Id, (int)this.StateId, (int)this.UserId, (DateTime)this.CheckIn, (DateTime)this.CheckOut, this.Type);

                //this._informer.InformSuccess("Event successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                 string.IsNullOrWhiteSpace(this.CheckIn.ToString()) ||
                 string.IsNullOrWhiteSpace(this.CheckOut.ToString())
            );
        }
    }
}
