using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicleWithPagination
{
    public class ListedLiabilityDto : IMapFrom<Liability>
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LiabilityType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Liability, ListedLiabilityDto>()
                .ForMember(d => d.LiabilityType, opt => opt.MapFrom(s => s.GetType().Name));
        }
    }
}
