using DotWatcher.Attributes;

namespace DotWatcher
{
    public enum ImageFormat
    {
        [FormatInfo(Description = "Bitmap", Extensions = ".bmp")]
        Bmp,

        [FormatInfo(Description = "GIF", Extensions = ".gif")]
        Gif,

        [FormatInfo(Description = "JPEG", Extensions = ".jpg, .jpeg")]
        Jpeg,

        [FormatInfo(Description = "PNG", Extensions = ".png")]
        Png,

        [FormatInfo(Description = "TIFF", Extensions = ".tiff")]
        Tiff
    }
}