using PresentationLayer.ViewModel;
using ServiceLayer;
using System;
using System.Configuration;
using System.Windows;
using Task2_v1_PresentationLayer.Model;
using Task2_v1_PresentationLayer.ViewModel;

namespace Task2_v1_PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModelBase viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize services
            string connectionString = "Data Source=TABLET-C23CIME9;Initial Catalog=Hotel;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
            IUserService userService = new UserService(connectionString);
            ICatalogService catalogService = new CatalogService(connectionString);
            IEventService eventService = new EventService(connectionString);
            IStateService stateService = new StateService(connectionString);

            // Create model data
            ModelDataAPI modelData = new ModelData(userService, catalogService, eventService, stateService);

            // Initialize the ViewModel with model data
            viewModel = new ViewModelMain(userService, catalogService, eventService, stateService);

            // Set DataContext
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
        }
    }
}
