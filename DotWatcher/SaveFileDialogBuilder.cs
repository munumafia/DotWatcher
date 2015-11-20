using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotWatcher.Parser;
using Microsoft.Win32;

namespace DotWatcher
{
    /// <summary>
    /// Builder used to build a new SaveFileDialog instance for saving something
    /// an image file
    /// </summary>
    public class SaveFileDialogBuilder
    {
        /// <summary>
        /// Builds a new SaveFileDialog instance for saving something as an image file
        /// </summary>
        /// <returns></returns>
        public SaveFileDialog Build()
        {
            var imageFormatEnumParser = new ImageFormatEnumParser();
            var enumFields = imageFormatEnumParser.Parse();

            var defaultFormat = enumFields
                .Where(ef => ef.Extensions.Contains(".png"))
                .Select((ef, idx) => new { Field = ef, Position = idx })
                .First();

            return new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".png",
                Filter = BuildFilterString(enumFields),
                FilterIndex = defaultFormat.Position,
                OverwritePrompt = true
            };
        }

        /// <summary>
        /// Creates the file filter string for the dialog based off of the <i>enumFields</i> parameter
        /// </summary>
        /// <param name="enumFields">List of enum fields and associated metadata describing the supported image formats</param>
        /// <returns>The file filter string</returns>
        private static string BuildFilterString(IEnumerable<ImageFormatEnumField> enumFields)
        {
            var builder = new StringBuilder();

            foreach (var field in enumFields)
            {
                var extensions = string.Join(";", field.Extensions
                    .Select(x => string.Format("*{0}", x))
                    .OrderBy(x => x));

                builder.AppendFormat("{0} ({1})|{1}|", field.Description, extensions);
            }

            builder.Append("All Files (*.*)|*.*");

            return builder.ToString();
        }
    }
}