using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Client.Wpf.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool showOnTrue;
            bool.TryParse(parameter.ToString(), out showOnTrue);

            if (showOnTrue)
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;

            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
