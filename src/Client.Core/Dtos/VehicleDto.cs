using System;

namespace Client.Core.Dtos
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int? Year { get; set; }
        public int Fuel { get; set; }
        public int VehicleType { get; set; }
        public int EngineDisplacement { get; set; }
        public int Mileage { get; set; }
        public string LicencePlate { get; set; } = string.Empty;
        public string Color { get; set; }
        public DateTime FirstRegistration { get; set; } = DateTime.Today;
        public int BeltMileage { get; set; }
        public int BrakeLiningsMileage { get; set; }
        public int BrakeDisksMileage { get; set; }
        public int CoolantMileage { get; set; }
        public int FuelConsumption { get; set; }
        public int OilMileage { get; set; }
        public string ImageName { get; set; }
        public string ImageAddress { get; set; }
        public int ActiveRecordEntryId { get; set; }
    }
}
