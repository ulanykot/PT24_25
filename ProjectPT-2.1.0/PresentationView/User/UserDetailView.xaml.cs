using System.Windows.Controls;
using PresentationViewModel;
namespace PresentationView;

/// <summary>
/// Interaction logic for UserDetailView.xaml
/// </summary>
public partial class UserDetailView : UserControl
{
    public UserDetailView()
    {
        this.DataContext = new UserDetailViewModel();
        InitializeComponent();
    }
}
