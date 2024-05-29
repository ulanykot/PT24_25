using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model.API;

namespace PresentationViewModel;

internal class UserDetailViewModel : IViewModel, IUserDetailViewModel
{
    public ICommand UpdateUser { get; set; }

    private readonly IUserModelOperation _modelOperation;

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

    private string _firstName;

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _lastName;

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
    private string _userType;

    public string UserType
    {
        get => _userType;
        set
        {
            _userType = value;
            OnPropertyChanged(nameof(UserType));
        }
    }

    public UserDetailViewModel(IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

        this._modelOperation = model ?? IUserModelOperation.CreateModelOperation();
        this._informer = informer ?? new PopupErrorInformer();
    }

    public UserDetailViewModel(int id, string firstName, string lastName, string userType, IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.UserType = userType;

        this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

        this._modelOperation = model ?? IUserModelOperation.CreateModelOperation();
        this._informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            this._modelOperation.UpdateAsync(this.Id, this.FirstName, this.LastName, this.UserType);

            this._informer.InformSuccess("User successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(this.FirstName) ||
            string.IsNullOrWhiteSpace(this.LastName)
        );
    }
}
