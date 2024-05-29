using System.Windows.Controls;
using PresentationViewModel;
namespace PresentationView;

/// <summary>
/// Interaction logic for ProductMasterView.xaml
/// </summary>
public partial class ProductMasterView : UserControl
{
    public ProductMasterView()
    {
        this.DataContext = new ProductMasterViewModel();
        InitializeComponent();
    }
}
