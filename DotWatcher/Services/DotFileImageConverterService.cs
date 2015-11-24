using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotWatcher.Parser;

namespace DotWatcher.Services
{
    /// <summary>
    /// Used to convert a dot file containing a graph definition to an image file
    /// </summary>
    public class DotFileImageConverterService : IDotFileImageConverterService
    {
        private readonly string _GraphVizBinPath;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="graphVizBinPath">The path to the GraphViz binary files</param>
        public DotFileImageConverterService(string graphVizBinPath)
        {
            _GraphVizBinPath = graphVizBinPath;

        }

        /// <summary>
        /// Converts a dot file to the requested image format
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file to convert</param>
        /// <param name="outputFilePath">The file path of where to save the image</param>
        /// <param name="imageFormat">The image format to use when converitng the dot file</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public Task ConvertAsync(string dotFilePath, string outputFilePath, ImageFormat imageFormat)
        {
            if (!File.Exists(dotFilePath))
            {
                throw new ArgumentException("The supplied dot file does not exist", "dotFilePath");
            }

            return Task.Run(() =>
            {
                const int sixtySeconds = 60 * 1000;
                var outputFormat = Enum.GetName(typeof(ImageFormat), (int)imageFormat).ToLower();

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Arguments = string.Format(@"-T{0} -o ""{1}"" ""{2}""", outputFormat, outputFilePath, dotFilePath),
                        CreateNoWindow = true,
                        FileName = Path.Combine(_GraphVizBinPath, "dot.exe"),
                        UseShellExecute = false
                    }
                };

                process.Start();
                process.WaitForExit(sixtySeconds);
            });
        }

        /// <summary>
        /// Converts a dot file to the requested image format
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file to convert</param>
        /// <param name="imageFormat">The image format to use when converting the dot file</param>
        /// <returns>The file path of the image that was generated</returns>
        public Task<string> ConvertAsync(string dotFilePath, ImageFormat imageFormat)
        {
            var outputFile = Path.GetTempFileName();
            return ConvertAsync(dotFilePath, outputFile, imageFormat).ContinueWith((task) => outputFile);
        }

        /// <summary>
        /// Converts a dot file to an image file, using the file extension of the <i>outputFilePath</i>
        /// parameter to determine the file format to use
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file to convert</param>
        /// <param name="outputFilePath">The file path of where to save the image</param>
        /// <returns>A task representing the async operation</returns>
        public Task ConvertAsync(string dotFilePath, string outputFilePath)
        {
            var fileinfo = new FileInfo(outputFilePath);
            var imageFormatEnumParser = new ImageFormatEnumParser();

            var imageFormat = imageFormatEnumParser.Parse()
                .Where(ef => ef.Extensions.Contains(fileinfo.Extension))
                .Select(ef => ef.Field.Name)
                .First();

            return ConvertAsync(dotFilePath, outputFilePath, (ImageFormat)Enum.Parse(typeof(ImageFormat), imageFormat));
        }
    }
}
