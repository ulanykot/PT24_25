using PresentationLayer.ViewModel;
using ServiceLayer;
using System;
using System.Configuration;
using System.Windows;
using Task2_v1_PresentationLayer.ViewModel;

namespace Task2_v1_PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ModelDataAPI modelData = new ModelData();
        private readonly ViewModelMain viewModel = new ViewModelMain(modelData);
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void FetchEventsButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedValue is int userId)
            {
                await viewModel.RefreshEventsForUser(userId);
            }
        }
        protected override void OnInitialized(EventArgs a)
        {
            base.OnInitialized(a);
            this.Loaded += (s, e) => { DataContext = this.viewModel; };
            //_vm.ChildWindow = () => new TreeViewMainWindow();
            //_vm.MessageBoxShowDelegate = text => MessageBox.Show(text, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        }
    }

