using System.Threading.Tasks;
using DotWatcher.Controls;
using DotWatcher.Services;

namespace DotWatcher.Builders
{
    /// <summary>
    /// Builder class for building a new instance of DotFileTabItem
    /// </summary>
    public class DotFileTabItemBuilder : IDotFileTabItemBuilder
    {
        private readonly IDotFileImageConverterService _DotFileImageConverterService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dotFileImageConverterService">The IDotFileImageConverterService instance to use</param>
        public DotFileTabItemBuilder(IDotFileImageConverterService dotFileImageConverterService)
        {
            _DotFileImageConverterService = dotFileImageConverterService;
        }

        /// <summary>
        /// Builds a new instance of DotFileTabItem asynchronously
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file that the tab should render</param>
        /// <returns>The DotFileTabItem instance</returns>
        public async Task<DotFileTabItem> BuildAsync(string dotFilePath)
        {
            var tabItem = new DotFileTabItem(_DotFileImageConverterService, dotFilePath);
            await tabItem.LoadAsync();

            return tabItem;
        }
    }
}
