using System.Windows;
using DotWatcher.Builders;
using DotWatcher.ViewModels;
using Microsoft.Win32;

namespace DotWatcher
{
    /// <summary>
    /// Code behind for the main window of the application
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISaveFileDialogBuilder _SaveFileDialogBuilder;
        private readonly IDotFileTabItemBuilder _DotFileTabItemBuilder;
        private readonly MainWindowViewModel _ViewModel;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="saveFileDialogBuilder">The ISaveFileDialogBuilder implementation to use</param>
        /// <param name="dotFileTabItemBuilder">The IDotFileTabItemBuilder implementation to use</param>
        public MainWindow(ISaveFileDialogBuilder saveFileDialogBuilder, IDotFileTabItemBuilder dotFileTabItemBuilder)
        {
            _SaveFileDialogBuilder = saveFileDialogBuilder;
            _DotFileTabItemBuilder = dotFileTabItemBuilder;

            InitializeComponent();

            DataContext = _ViewModel = new MainWindowViewModel();
        }

        /// <summary>
        /// Event handler that opens a file dialog when the "Open" file menu item is
        /// clicked
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event arguments</param>
        private async void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                DefaultExt = ".dot",
                Filter = "DOT Files(*.dot;*.gv)|*.dot;*.gv|All files (*.*)|*.*",
                Multiselect = true
            };

            var filesSelected = openDialog.ShowDialog();
            if (filesSelected == false)
            {
                return;
            }

            foreach (var file in openDialog.FileNames)
            {
                var tab = await _DotFileTabItemBuilder.BuildAsync(file);
                _ViewModel.DotFileTabs.Add(tab);
            }
        }

        /// <summary>
        /// Event handler that opens a file dialog for saving the image of the selected dot file
        /// tab when the "Save As" file menu item is clicked
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event arguments</param>
        private async void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = _SaveFileDialogBuilder.Build();

            var result = dialog.ShowDialog();
            if (result != false && Tabs.SelectedTab != null)
            {
                await Tabs.SelectedTab.SaveImageAsync(dialog.FileName);
            }
        }
    }
}