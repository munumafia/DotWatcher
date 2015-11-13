
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DotWatcher
{
    public class DotFileImageConverter
    {
        private readonly string _ToolPath;

        public DotFileImageConverter(string toolPath)
        {
            _ToolPath = toolPath;
        }

        public Task ConvertAsync(string dotFile, string outputFile, ImageFormat imageFormat)
        {
            if (!File.Exists(dotFile))
            {
                throw new ArgumentException("The supplied dot file does not exist", "dotFile");
            }

            return Task.Run(() =>
            {
                const int sixtySeconds = 60 * 1000;
                var outputFormat = Enum.GetName(typeof(ImageFormat), (int)imageFormat).ToLower();

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Arguments = string.Format(@"-T{0} -o ""{1}"" ""{2}""", outputFormat, outputFile, dotFile),
                        CreateNoWindow = true,
                        FileName = Path.Combine(_ToolPath, "dot.exe"),

                    }
                };

                process.Start();
                process.WaitForExit(sixtySeconds);
            });
        }

        public Task<string> ConvertAsync(string dotFile, ImageFormat imageFormat)
        {
            var outputFile = Path.GetTempFileName();
            return ConvertAsync(dotFile, outputFile, imageFormat).ContinueWith((task) => outputFile);
        }
    }
}
