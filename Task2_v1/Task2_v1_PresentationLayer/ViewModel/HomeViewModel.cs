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
    internal class HomeViewModel : IViewModel
    {
        public ICommand StartAppCommand { get; set; }

        public ICommand ExitAppCommand { get; set; }

        public HomeViewModel()
        {
            this.StartAppCommand = new SwitchViewCommand("CatalogMasterView");

            this.ExitAppCommand = new CloseApplicationCommand();
        }
    }
}
