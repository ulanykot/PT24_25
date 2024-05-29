using System.Windows.Controls;
using PresentationViewModel;
namespace PresentationView;

/// <summary>
/// Interaction logic for StateMasterView.xaml
/// </summary>
public partial class StateMasterView : UserControl
{
    public StateMasterView()
    {
        this.DataContext = new StateMasterView();
        InitializeComponent();
    }
}
