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
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.Date.Add(s.Duration)))
                .ForMember(d => d.LiabilityType, opt => opt.MapFrom(s => s.GetType().Name));
        }
    }
}
