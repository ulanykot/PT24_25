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

namespace PresentationLayer.ViewModel.DetailView
{
    internal class DetailUser : IViewModel
    {
        public ICommand UpdateUser { get; set; }

        private readonly IUserModel _modelOperation;

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

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }
        private string _userType;
        public string UserType
        {
            get => _userType;
            set
            {
                _userType = value;
                RaisePropertyChanged(nameof(UserType));
            }
        }

        public DetailUser(IUserModel model = null)
        {
            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? ModelFactory.CreateUserModelOperation();
            //this._informer = informer ?? new PopupErrorInformer();
        }

        public DetailUser(int id, string firstName, string lastName, string type, IUserModel model = null)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserType = type;

            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelOperation = model ?? ModelFactory.CreateUserModelOperation();
            //this._informer = informer ?? new PopupErrorInformer();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelOperation.UpdateAsync(this.Id, this.FirstName, this.LastName, this.UserType);

                //this._informer.InformSuccess("User successfully updated!");
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.FirstName) ||
                string.IsNullOrWhiteSpace(this.LastName) ||
                string.IsNullOrWhiteSpace(this.UserType)
            );
        }
    }
}
