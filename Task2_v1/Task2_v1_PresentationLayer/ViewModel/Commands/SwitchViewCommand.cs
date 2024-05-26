using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using PresentationLayer.ViewModel.MasterView;

namespace PresentationLayer.ViewModel.Commands
{
    internal class SwitchViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private string _switchToViewModel;

        public SwitchViewCommand(string viewModel)
        {
            this._switchToViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            UserControl userControl = parameter as UserControl;

            Window parentWindow = Window.GetWindow(userControl);

            if (parentWindow != null)
            {
                if (parentWindow.DataContext is MainWindowViewModel mainViewModel)
                {
                    switch (this._switchToViewModel)
                    {
                        case "CatalogMasterView":
                            mainViewModel.SelectedViewModel = new MasterCatalog(); break;
                        case "UserMasterView":
                            mainViewModel.SelectedViewModel = new MasterUser(); break;
                        case "StateMasterView":
                            mainViewModel.SelectedViewModel = new MasterState(); break;
                        case "EventMasterView":
                            mainViewModel.SelectedViewModel = new MasterEvent(); break;
                    }
                }
            }
        }
    }
}
