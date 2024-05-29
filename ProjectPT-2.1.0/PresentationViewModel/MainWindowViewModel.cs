namespace PresentationViewModel;

public class MainWindowViewModel : IViewModel
{
    private IViewModel _selectedViewModel { get; set; }


    public MainWindowViewModel()
    {
        _selectedViewModel = new HomeViewModel();
    }

    public new IViewModel SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;

            OnPropertyChanged(nameof(SelectedViewModel));
        }
    }
}
