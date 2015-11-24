using System.Threading.Tasks;

namespace DotWatcher.Services
{
    public interface IDotFileImageConverterService
    {
        /// <summary>
        /// Converts a dot file to the requested image format
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file to convert</param>
        /// <param name="outputFilePath">The file path of where to save the image</param>
        /// <param name="imageFormat">The image format to use when converitng the dot file</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task ConvertAsync(string dotFilePath, string outputFilePath, ImageFormat imageFormat);

        /// <summary>
        /// Converts a dot file to the requested image format
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file to convert</param>
        /// <param name="imageFormat">The image format to use when converting the dot file</param>
        /// <returns>The file path of the image that was generated</returns>
        Task<string> ConvertAsync(string dotFilePath, ImageFormat imageFormat);

        /// <summary>
        /// Converts a dot file to an image file, using the file extension of the <i>outputFilePath</i>
        /// parameter to determine the file format to use
        /// </summary>
        /// <param name="dotFilePath">The path to the dot file to convert</param>
        /// <param name="outputFilePath">The file path of where to save the image</param>
        /// <returns>A task representing the async operation</returns>
        Task ConvertAsync(string dotFilePath, string outputFilePath);
    }
}