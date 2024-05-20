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

            // Initialize the ViewModel with model data
            viewModel = new ViewModelMain();

            // Set DataContext
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
        }
    }
}
