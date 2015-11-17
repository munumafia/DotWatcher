using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotWatcher.Parser;
using Microsoft.Win32;

namespace DotWatcher
{
    public class SaveFileDialogBuilder
    {
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

        private static string BuildFilterString(IList<ImageFormatEnumField> enumFields)
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