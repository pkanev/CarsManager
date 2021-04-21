using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Client.Wpf.Converters
{
    public class PriceToDecimalsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (decimal)value;
            return number == 0
                ? "0"
                : number.ToString("#.##", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            if (string.IsNullOrEmpty(s))
                return 0M;

            return decimal.Parse(s);
        }
    }
}
