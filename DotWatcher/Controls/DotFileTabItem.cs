using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DotWatcher.Annotations;
using DotWatcher.Services;

namespace DotWatcher.Controls
{
    /// <summary>
    /// Represents a tab in the DotFileTabControl user control
    /// </summary>
    public class DotFileTabItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised everytime a view model property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _ContentUpdated;
        private string _DotFilePath;
        private string _ImagePath;
        private bool _IsSelected;
        private readonly IDotFileImageConverterService _DotFileImageConverterService;
        private readonly FileSystemWatcher _DotFileWatcher;

        /// <summary>
        /// Whether or not the tab content has been updated and needs to be
        /// shown to the user
        /// </summary>
        public bool ContentUpdated
        {
            get { return _ContentUpdated; }
            set
            {
                _ContentUpdated = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The path to the dot file that the tab is showing an image for
        /// </summary>
        public string DotFilePath
        {
            get { return _DotFilePath; }
            set
            {
                _DotFilePath = value;

                OnPropertyChanged();
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// The path to the dot file image that the tab item generated and is
        /// showing
        /// </summary>
        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;

                OnPropertyChanged();
                OnPropertyChanged("ContentUpdated");
            }
        }

        /// <summary>
        /// Whether or not the tab is currently selected by the DotFileTabControl
        /// </summary>
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The title of the tab item
        /// </summary>
        public string Title
        {
            get
            {
                return !string.IsNullOrEmpty(DotFilePath)
                    ? new FileInfo(DotFilePath).Name
                    : string.Empty;
            }
        }

        /// <summary>
        /// Constructs a new DotFileTabItem
        /// </summary>
        /// <param name="dotFileImageConverterService">The IDotFileImageConverterService implementation to use</param>
        /// <param name="dotFilePath">The file path to the dot file to show in the tab</param>
        public DotFileTabItem(IDotFileImageConverterService dotFileImageConverterService, string dotFilePath)
        {
            _DotFileImageConverterService = dotFileImageConverterService;
            DotFilePath = dotFilePath;

            _DotFileWatcher = new FileSystemWatcher();
            _DotFileWatcher.Changed += OnDotFileChanged;
        }

        /// <summary>
        /// Event handler used to regenerate the image shown in the tab whenever the dot file is changed
        /// on disk
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event arguments</param>
        private async void OnDotFileChanged(object sender, FileSystemEventArgs e)
        {
            await LoadAsync();
        }

        /// <summary>
        /// Loads the contents of the tab
        /// </summary>
        /// <returns>Task representing the async operation</returns>
        public async Task LoadAsync()
        {
            var imageFormat = (ImageFormat)Enum.Parse(typeof(ImageFormat), ConfigurationManager.AppSettings["outputFormat"], true);
            ImagePath = await _DotFileImageConverterService.ConvertAsync(DotFilePath, imageFormat);

            var fileInfo = new FileInfo(DotFilePath);

            _DotFileWatcher.EnableRaisingEvents = false;
            _DotFileWatcher.Path = fileInfo.DirectoryName;
            _DotFileWatcher.Filter = fileInfo.Name;
            _DotFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _DotFileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Saves the dot file image rendered by the tab to the specified file path
        /// </summary>
        /// <param name="filename">The filename to save the image as</param>
        /// <returns>Task representing the async operation</returns>
        public async Task SaveImageAsync(string filename)
        {
            await _DotFileImageConverterService.ConvertAsync(DotFilePath, filename);
        }

        /// <summary>
        /// Method used to raise a PropertyChanged event when a view model property changes
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
