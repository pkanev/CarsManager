using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Repairs.Queries.Dtos
{
    public class RepairDto : IMapFrom<Repair>
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicencePlate { get; set; }
        public int RepairShopId { get; set; }
        public string RepairShopName { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
        public bool IsOilChanged { get; set; }
        public bool IsBeltChanged { get; set; }
        public bool IsBatteryChanged { get; set; }
        public bool IsSparkPlugChanged { get; set; }
        public bool IsFuelFilterChanged { get; set; }
        public bool IsBrakeLiningsChanged { get; set; }
        public bool IsBrakeDisksChanged { get; set; }
        public bool IsCoolantChanged { get; set; }
        public bool IsOtherWorkDone { get; set; }
        public string Description { get; set; }
        public decimal InitialPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Repair, RepairDto>()
                .ForMember(d => d.VehicleLicencePlate, opt => opt.MapFrom(s => s.Vehicle.LicencePlate))
                .ForMember(d => d.RepairShopName, opt => opt.MapFrom(s => s.RepairShop.Name));
        }
    }
}
