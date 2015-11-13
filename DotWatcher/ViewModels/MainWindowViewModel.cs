using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DotWatcher.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
