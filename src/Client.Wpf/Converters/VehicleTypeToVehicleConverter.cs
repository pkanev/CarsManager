using System;
using System.Globalization;
using System.Windows.Data;
using Client.Core.Data;

namespace Client.Wpf.Converters
{
    class VehicleTypeToVehicleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => VehicleResources.GetVehicleType((int)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
