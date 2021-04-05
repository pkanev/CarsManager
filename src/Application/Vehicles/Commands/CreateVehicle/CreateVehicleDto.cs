using System;
using CarsManager.Application.Vehicles.Commands.Dtos;
using CarsManager.Domain.Enums;

namespace CarsManager.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleDto
    {
        public int ModelId { get; set; }
        public int? Year { get; set; }
        public FuelType Fuel { get; set; }
        public int EngineDisplacement { get; set; }
        public int Mileage { get; set; }
        public string LicencePlate { get; set; }
        public string Color { get; set; }
        public DateTime FirstRegistration { get; set; }
        public int BeltMileage { get; set; }
        public int BrakeLiningsMileage { get; set; }
        public int BrakeDisksMileage { get; set; }
        public int CoolantMileage { get; set; }
        public int FuelConsumption { get; set; }
        public int OilMileage { get; set; }
        public string ImageName { get; set; }
        public LiabilityDto MOT { get; set; }
        public LiabilityDto CivilLiability { get; set; }
        public LiabilityDto CarInsurance { get; set; }
        public LiabilityDto Vignette { get; set; }
    }
}
