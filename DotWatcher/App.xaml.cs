using System.Configuration;
using System.Windows;
using DotWatcher.Services;
using StructureMap;
using StructureMap.Graph;

namespace DotWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                });

                _.For<IDotFileImageConverterService>().Use("Construct DotFileImageConverterService with toolpath", ctx =>
                {
                    var toolPath = ConfigurationManager.AppSettings["ToolPath"];
                    return new DotFileImageConverterService(toolPath);
                });
            });

            MainWindow = container.GetInstance<MainWindow>();
            MainWindow.ShowDialog();

            base.OnStartup(e);
        }
    }
}