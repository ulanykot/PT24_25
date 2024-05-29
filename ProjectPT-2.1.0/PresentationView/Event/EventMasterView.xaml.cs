using PresentationViewModel;
using System.Windows.Controls;

namespace PresentationView;

/// <summary>
/// Interaction logic for EventMasterView.xaml
/// </summary>
public partial class EventMasterView : UserControl
{
    public EventMasterView()
    {
        this.DataContext = new EventMasterViewModel();
        InitializeComponent();
    }
}
