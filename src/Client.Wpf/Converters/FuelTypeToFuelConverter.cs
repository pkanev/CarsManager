using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Client.Core.Data;

namespace Client.Wpf.Converters
{
    public class FuelTypeToFuelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fuelType = (int)value;
            return VehicleResources.FuelTypes.Count > fuelType
                ? VehicleResources.FuelTypes[fuelType].Name
                : VehicleResources.FuelTypes.First().Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
