using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Repairs.Queries.GetRepairsForVehicleWithPagination
{
    public class ListedRepairForVehicleDto : IMapFrom<Repair>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string RepairShopName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Repair, ListedRepairForVehicleDto>()
                .ForMember(d => d.RepairShopName, opt => opt.MapFrom(s => s.RepairShop.Name));
        }
    }
}
