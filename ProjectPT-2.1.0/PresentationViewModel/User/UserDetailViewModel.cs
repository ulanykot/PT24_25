using System;
using System.Threading.Tasks;
using System.Windows.Input;

using PresentationModel;

namespace PresentationViewModel;

public class UserDetailViewModel : IViewModel, IUserDetailViewModel
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
        UpdateUser = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? IUserModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    public UserDetailViewModel(int id, string firstName, string lastName, string userType, IUserModelOperation? model = null, IErrorInformer? informer = null)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;

        UpdateUser = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? IUserModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            _modelOperation.UpdateAsync(Id, FirstName, LastName, UserType);

            _informer.InformSuccess("User successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName)
        );
    }
}
