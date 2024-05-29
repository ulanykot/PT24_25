using System.Windows.Controls;
using PresentationViewModel;
namespace PresentationView;

/// <summary>
/// Interaction logic for StateDetailView.xaml
/// </summary>
public partial class StateDetailView : UserControl
{
    public StateDetailView()
    {
        this.DataContext = new StateDetailViewModel();
        InitializeComponent();
    }
}
