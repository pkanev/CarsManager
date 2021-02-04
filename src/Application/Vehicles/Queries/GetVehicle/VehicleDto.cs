using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehicle
{
    public class VehicleDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int Year { get; set; }
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
        
        //public ICollection<Employee> Employees { get; private set; } = new HashSet<Employee>();
        //public ICollection<Repair> Repairs { get; private set; } = new HashSet<Repair>();
        //public ICollection<MOT> MOTs { get; private set; } = new HashSet<MOT>();
        //public ICollection<CivilLiability> CivilLiabilities { get; private set; } = new HashSet<CivilLiability>();
        //public ICollection<CarInsurance> CarInsurances { get; private set; } = new HashSet<CarInsurance>();
        //public ICollection<Vignette> Vignettes { get; private set; } = new HashSet<Vignette>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleDto>()
                .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
                .ForMember(d => d.ModelId, opt => opt.MapFrom(s => s.Model.Id))
                .ForMember(d => d.MakeName, opt => opt.MapFrom(s => s.Model.Make.Name))
                .ForMember(d => d.MakeId, opt => opt.MapFrom(s => s.Model.Make.Id));
        }
    }
}
