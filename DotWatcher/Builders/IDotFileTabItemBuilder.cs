using System.Threading.Tasks;
using DotWatcher.Controls;

namespace DotWatcher.Builders
{
    public interface IDotFileTabItemBuilder
    {
        /// <summary>
        /// Builds a new instance of DotFileTabItem asynchronously
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file that the tab should render</param>
        /// <returns>The DotFileTabItem instance</returns>
        Task<DotFileTabItem> BuildAsync(string dotFilePath);
    }
}