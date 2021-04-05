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
        public int Fuel { get; set; }
        public int VehicleType { get; set; }
        public int EngineDisplacement { get; set; }
        public int? Year { get; set; }
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
        public string ImageAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleDto>()
                .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
                .ForMember(d => d.ModelId, opt => opt.MapFrom(s => s.Model.Id))
                .ForMember(d => d.MakeName, opt => opt.MapFrom(s => s.Model.Make.Name))
                .ForMember(d => d.MakeId, opt => opt.MapFrom(s => s.Model.Make.Id))
                .ForMember(d => d.Fuel, opt => opt.MapFrom(s => (int)s.Fuel))
                .ForMember(d => d.VehicleType, opt => opt.MapFrom(s => (int)s.Model.VehicleType))
                .ForMember(d => d.ImageName, opt => opt.MapFrom(s => s.Image));
        }
    }
}
