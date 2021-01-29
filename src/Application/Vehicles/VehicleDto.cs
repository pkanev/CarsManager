using System;
using System.Collections.Generic;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles
{
    public class VehicleDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public Model Model { get; set; }
        public DateTime Year { get; set; }
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
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        //public ICollection<Repair> Repairs { get; private set; } = new HashSet<Repair>();
        //public ICollection<MOT> MOTs { get; private set; } = new HashSet<MOT>();
        //public ICollection<CivilLiability> CivilLiabilities { get; private set; } = new HashSet<CivilLiability>();
        //public ICollection<CarInsurance> CarInsurances { get; private set; } = new HashSet<CarInsurance>();
        //public ICollection<Vignette> Vignettes { get; private set; } = new HashSet<Vignette>();

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<Vehicle, VehicleDto>()
        //        .ForMember(d => d.Model.VehicleType, opt => opt.MapFrom(v => (int)v.Model.VehicleType));
        //}
    }
}
