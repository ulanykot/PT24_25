using System.Windows.Input;

namespace PresentationViewModel;

public class HomeViewModel : IViewModel
{
    public ICommand StartAppCommand { get; set; }

    public ICommand ExitAppCommand { get; set; }

    public HomeViewModel()
    {
        StartAppCommand = new SwitchViewCommand("ProductMasterView");

        ExitAppCommand = new CloseApplicationCommand();
    }
}
