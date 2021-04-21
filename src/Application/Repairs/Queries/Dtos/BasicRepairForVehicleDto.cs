using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Repairs.Queries.Dtos
{
    public class BasicRepairForVehicleDto : IMapFrom<Repair>
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int RepairShopId { get; set; }
        public string RepairShopName { get; set; }
        public DateTime Date { get; set; }
        public int Mileage { get; set; }
        public string Description { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Repair, BasicRepairForVehicleDto>()
                .ForMember(d => d.RepairShopName, opt => opt.MapFrom(s => s.RepairShop.Name));
        }
    }
}
