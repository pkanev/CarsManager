using System;
using System.Collections.Generic;
using CarsManager.Domain.Enums;

namespace CarsManager.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
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
        public string Image { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public ICollection<Repair> Repairs { get; private set; } = new HashSet<Repair>();
        public ICollection<MOT> MOTs { get; private set; } = new HashSet<MOT>();
        public ICollection<CivilLiability> CivilLiabilities { get; private set; } = new HashSet<CivilLiability>();
        public ICollection<CarInsurance> CarInsurances { get; private set; } = new HashSet<CarInsurance>();
        public ICollection<Vignette> Vignettes { get; private set; } = new HashSet<Vignette>();
        public ICollection<RoadBookEntry> RoadBookEntries { get; private set; } = new HashSet<RoadBookEntry>();
    }
}
