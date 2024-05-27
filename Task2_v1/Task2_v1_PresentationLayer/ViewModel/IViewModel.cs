using System.ComponentModel;

namespace Task2_v1_PresentationLayer.ViewModel
{
    public class IViewModel : INotifyPropertyChanged
    {
        public IViewModel SelectedViewModel;

        public IViewModel Parent { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
