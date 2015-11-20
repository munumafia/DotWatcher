using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DotWatcher.Controls;

namespace DotWatcher.ViewModels
{
    /// <summary>
    /// View model for the main window of the application
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DotFileTabItem> _DotFileTabs;

        /// <summary>
        /// Event raised everytime a view model property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Collection of tabs representing the open dot files on the screen
        /// </summary>
        public ObservableCollection<DotFileTabItem> DotFileTabs
        {
            get { return _DotFileTabs; }
            set
            {
                _DotFileTabs = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            DotFileTabs = new ObservableCollection<DotFileTabItem>();
        }

        /// <summary>
        /// Method used to raise a PropertyChanged event when a view model property changes
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
