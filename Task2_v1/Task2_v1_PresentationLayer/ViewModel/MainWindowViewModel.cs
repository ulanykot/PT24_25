using PresentationLayer.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task2_v1_PresentationLayer.ViewModel;

namespace PresentationLayer.ViewModel
{
    internal class MainWindowViewModel : IViewModel
    {
            public ICommand StartAppCommand { get; set; }
            public ICommand ExitAppCommand { get; set; }

            private IViewModel _selectedViewModel;

            public new IViewModel SelectedViewModel
            {
                get => _selectedViewModel;
                set
                {
                    _selectedViewModel = value;
                    RaisePropertyChanged(nameof(SelectedViewModel));
                }
            }

            public MainWindowViewModel()
            {
                this.StartAppCommand = new SwitchViewCommand("CatalogMasterView");
                this.ExitAppCommand = new CloseApplicationCommand();
            }
        }
    }



