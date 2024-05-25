using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer;
using Task2_v1_PresentationLayer.Model;

namespace Task2_v1_PresentationLayer.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase SelectedViewModel;

        public ViewModelBase Parent { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
