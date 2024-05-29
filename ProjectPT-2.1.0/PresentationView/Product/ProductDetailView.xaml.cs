using System.Windows.Controls;
using PresentationViewModel;
namespace PresentationView;

/// <summary>
/// Interaction logic for ProductDetailView.xaml
/// </summary>
public partial class ProductDetailView : UserControl
{
    public ProductDetailView()
    {
        this.DataContext = new ProductDetailViewModel();
        InitializeComponent();
    }
}
