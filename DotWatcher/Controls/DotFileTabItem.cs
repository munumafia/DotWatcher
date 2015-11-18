using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DotWatcher.Annotations;

namespace DotWatcher.Controls
{
    public class DotFileTabItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _ContentUpdated;
        private string _DotFilePath;
        private string _ImagePath;
        private bool _IsSelected;
        private readonly DotFileImageConverter _DotFileImageConverter;
        private readonly FileSystemWatcher _DotFileWatcher;

        public bool ContentUpdated
        {
            get { return _ContentUpdated; }
            set
            {
                _ContentUpdated = value;
                OnPropertyChanged();
            }
        }

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

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return !string.IsNullOrEmpty(DotFilePath)
                    ? new FileInfo(DotFilePath).Name
                    : string.Empty;
            }
        }

        public DotFileTabItem(string dotFilePath)
        {
            _DotFileWatcher = new FileSystemWatcher();
            _DotFileWatcher.Changed += OnDotFileChanged;

            _DotFileImageConverter = new DotFileImageConverter(
                ConfigurationManager.AppSettings["ToolPath"]);

            DotFilePath = dotFilePath;
        }

        private async void OnDotFileChanged(object sender, FileSystemEventArgs e)
        {
            await LoadAsync();
        }

        public async Task LoadAsync()
        {
            var imageFormat = (ImageFormat)Enum.Parse(typeof(ImageFormat), ConfigurationManager.AppSettings["outputFormat"], true);
            ImagePath = await _DotFileImageConverter.ConvertAsync(DotFilePath, imageFormat);

            var fileInfo = new FileInfo(DotFilePath);

            _DotFileWatcher.EnableRaisingEvents = false;
            _DotFileWatcher.Path = fileInfo.DirectoryName;
            _DotFileWatcher.Filter = fileInfo.Name;
            _DotFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _DotFileWatcher.EnableRaisingEvents = true;
        }

        public async Task SaveImageAsync(string filename)
        {
            await _DotFileImageConverter.ConvertAsync(DotFilePath, filename);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
