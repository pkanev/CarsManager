using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Client.Wpf.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool redOnTrue;
            bool.TryParse(parameter?.ToString(), out redOnTrue);

            if (redOnTrue)
                return (bool)value ? Brushes.Red : Brushes.Black;

            return (bool)value ? Brushes.Black : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
