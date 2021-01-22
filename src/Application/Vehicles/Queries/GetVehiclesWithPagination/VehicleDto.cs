using System;
using System.Collections.Generic;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;
using CarsManager.Domain.Enums;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination
{
    public class VehicleDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public Model Model { get; set; }
        public DateTime Year { get; set; }
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
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Repair> Repairs { get; set; }
        public ICollection<MOT> MOTs { get; set; }
        public ICollection<CivilLiability> CivilLiabilities { get; set; }
        public ICollection<CarInsurance> CarInsurances { get; set; }
        public ICollection<Vignette> Vignettes { get; set; }
    }
}
