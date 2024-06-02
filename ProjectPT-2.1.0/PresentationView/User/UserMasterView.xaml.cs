using System.Windows.Controls;
using PresentationViewModel;
namespace PresentationView;

/// <summary>
/// Interaction logic for UserMasterView.xaml
/// </summary>
public partial class UserMasterView : UserControl
{
    public UserMasterView()
    {
        this.DataContext = new UserMasterViewModel();
        InitializeComponent();
    }
}
