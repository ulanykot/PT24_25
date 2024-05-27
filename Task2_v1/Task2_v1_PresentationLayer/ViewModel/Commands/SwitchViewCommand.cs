using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using PresentationLayer.ViewModel.MasterView;
using Task2_v1_PresentationLayer;
using System.Linq.Expressions;

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
            if (Application.Current.MainWindow?.DataContext is MainWindowViewModel mainViewModel)
            {
                switch (_switchToViewModel)
                {
                    case "CatalogMasterView":
                        mainViewModel.SelectedViewModel = new MasterCatalog(); break;
                    case "UserMasterView":
                        mainViewModel.SelectedViewModel = new MasterUser(); break;
                    case "StateMasterView":
                        mainViewModel.SelectedViewModel = new MasterState(); break;
                    case "EventMasterView":
                        mainViewModel.SelectedViewModel = new MasterEvent(); break;
                    default:
                        throw new ArgumentException("Unknown ViewModel type");
                }
            } else
            {
                throw new Exception(_switchToViewModel);
            }
        }

    }
}
