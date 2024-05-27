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
        private IViewModel _selectedViewModel { get; set; }


        public MainWindowViewModel()
        {
            this._selectedViewModel = new HomeViewModel();
        }

        public new IViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;

                RaisePropertyChanged(nameof(SelectedViewModel));
            }
        }
    }
}



