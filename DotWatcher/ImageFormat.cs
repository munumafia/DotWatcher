using DotWatcher.Attributes;

namespace DotWatcher
{
    /// <summary>
    /// Enum containing the file formats supported by the application
    /// </summary>
    public enum ImageFormat
    {
        /// <summary>
        /// Bitmap image format
        /// </summary>
        [FormatInfo(Description = "Bitmap", Extensions = ".bmp")]
        Bmp,

        /// <summary>
        /// GIF image format
        /// </summary>
        [FormatInfo(Description = "GIF", Extensions = ".gif")]
        Gif,

        /// <summary>
        /// JPEG image format
        /// </summary>
        [FormatInfo(Description = "JPEG", Extensions = ".jpg, .jpeg")]
        Jpeg,

        /// <summary>
        /// PNG image format
        /// </summary>
        [FormatInfo(Description = "PNG", Extensions = ".png")]
        Png,

        /// <summary>
        /// TIFF image format
        /// </summary>
        [FormatInfo(Description = "TIFF", Extensions = ".tiff")]
        Tiff
    }
}