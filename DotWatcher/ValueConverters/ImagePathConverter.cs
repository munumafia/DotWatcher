using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DotWatcher.ValueConverters
{
    /// <summary>
    /// Converts a string containing a file path to a BitmapImage instance
    /// </summary>
    public class ImagePathConverter : BaseConverter, IValueConverter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>
        /// Visual Studio was showing a warning about there not being a default public constructor, since the 
        /// default constructor inherited from BaseConverter is protected. This was added as a work-around to
        /// keep Visual Studio from driving me nuts
        /// </remarks>
        public ImagePathConverter()
        {
        }

        /// <summary>
        /// Convert the path string to a BitmapImage instance
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType">The target type</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture to use</param>
        /// <returns>The BitmapImage instance</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var image = new BitmapImage(new Uri(path, UriKind.Absolute));
            image.Freeze();

            return image;
        }

        /// <summary>
        /// Convert the BitmapImage instance back to a path string
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType">The target type</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture to use</param>
        /// <returns>Null since no conversion exists</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}