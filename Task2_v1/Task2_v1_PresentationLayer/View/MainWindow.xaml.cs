using PresentationLayer.ViewModel;
using ServiceLayer;
using System;
using System.Configuration;
using System.Windows;

namespace PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
    }
}

