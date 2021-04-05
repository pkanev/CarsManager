using System.Collections.Generic;
using System.Drawing;
using Client.Core.Models;

namespace Client.Core.Data
{
    public static class VehicleResources
    {
        public static List<Color> Colors = new List<Color>
        {
            Color.White,
            Color.Gray,
            Color.Black,
            Color.Tan,
            Color.Brown,
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Purple,
        };

        public static List<FuelTypeModel> FuelTypes = new List<FuelTypeModel>
        {
            new FuelTypeModel{Id = 0, Name = "Бензин"},
            new FuelTypeModel{Id = 1, Name = "Дизел"},
            new FuelTypeModel{Id = 2, Name = "Пропан"},
            new FuelTypeModel{Id = 3, Name = "Етанол"},
            new FuelTypeModel{Id = 4, Name = "Био дизел"},
        };

        public static List<VehicleTypeModel> VehicleTypes = new List<VehicleTypeModel>
        {
            new VehicleTypeModel{Id = 0, Name = "Лека"},
            new VehicleTypeModel{Id = 1, Name = "Джип"},
            new VehicleTypeModel{Id = 2, Name = "Камион"},
            new VehicleTypeModel{Id = 3, Name = "Автобус"},
        };
    }
}