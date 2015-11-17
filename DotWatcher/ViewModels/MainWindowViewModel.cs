using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DotWatcher.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _DotFilePath;
        private string _ImagePath;

        public bool DotFileOpened
        {
            get { return !string.IsNullOrEmpty(DotFilePath); }
        }

        public string DotFilePath
        {
            get { return _DotFilePath; }
            set
            {
                _DotFilePath = value;
                OnPropertyChanged();
                OnPropertyChanged("DotFileOpened");
            }
        }

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
