using PresentationViewModel;
using System.Windows.Controls;

namespace PresentationView;

/// <summary>
/// Interaction logic for EventDetailView.xaml
/// </summary>
public partial class EventDetailView : UserControl
{
    public EventDetailView()
    {
        this.DataContext = new EventDetailViewModel();
        InitializeComponent();
    }
}
