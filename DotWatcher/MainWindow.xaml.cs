using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using DotWatcher.ViewModels;
using Microsoft.Win32;

namespace DotWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DotFileImageConverter _DotFileImageConverter;
        private readonly FileSystemWatcher _DotFileWatcher;
        private readonly MainWindowViewModel _ViewModel;


        public MainWindow()
        {
            InitializeComponent();

            DataContext = _ViewModel = new MainWindowViewModel();

            _DotFileImageConverter = new DotFileImageConverter(ConfigurationManager.AppSettings["toolPath"]);

            _DotFileWatcher = new FileSystemWatcher();
            _DotFileWatcher.Changed += OnDotFileChanged;
        }

        private async Task HandleDotFileChanged(string filePath)
        {
            var imageFormat = (ImageFormat)Enum.Parse(typeof(ImageFormat), ConfigurationManager.AppSettings["outputFormat"], true);
            var imagePath = await _DotFileImageConverter.ConvertAsync(filePath, imageFormat);

            _ViewModel.DotFilePath = filePath;
            _ViewModel.ImagePath = imagePath;
        }

        private async void OnDotFileChanged(object sender, FileSystemEventArgs e)
        {
            await HandleDotFileChanged(e.FullPath);
        }

        private async void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                DefaultExt = ".dot",
                Filter = "DOT Files(*.dot;*.gv)|*.dot;*.gv|All files (*.*)|*.*"
            };

            var fileSelected = openDialog.ShowDialog();
            if (fileSelected == false)
            {
                return;
            }

            var fileInfo = new FileInfo(openDialog.FileName);

            _DotFileWatcher.EnableRaisingEvents = false;
            _DotFileWatcher.Path = fileInfo.DirectoryName;
            _DotFileWatcher.Filter = fileInfo.Name;
            _DotFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _DotFileWatcher.EnableRaisingEvents = true;

            await HandleDotFileChanged(fileInfo.FullName);
        }

        private async void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialogBuilder = new SaveFileDialogBuilder();
            var dialog = dialogBuilder.Build();

            var result = dialog.ShowDialog();
            if (result == false)
            {
                return;
            }

            await _DotFileImageConverter.ConvertAsync(_ViewModel.DotFilePath, dialog.FileName);
        }
    }
}