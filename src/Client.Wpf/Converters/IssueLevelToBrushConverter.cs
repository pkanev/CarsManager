using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Client.Core.Models.Issues;

namespace Client.Wpf.Converters
{
    public class IssueLevelToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (IssueLevel)value;
            if (data == IssueLevel.Ok)
                return Brushes.Green;

            if (data == IssueLevel.Warning)
                return Brushes.Orange;

            if (data == IssueLevel.Alert)
                return Brushes.Red;

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
