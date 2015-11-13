using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DotWatcher.ValueConverters
{
    public class ImagePathConverter : BaseConverter, IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
