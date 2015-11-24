
using Microsoft.Win32;

namespace DotWatcher.Builders
{
    // Interface for the SaveFileDialogBuilder class
    public interface ISaveFileDialogBuilder
    {
        /// <summary>
        /// Builds a new instance of SaveFileDialog
        /// </summary>
        /// <returns>The SaveFileDialog instance that was built</returns>
        SaveFileDialog Build();
    }
}