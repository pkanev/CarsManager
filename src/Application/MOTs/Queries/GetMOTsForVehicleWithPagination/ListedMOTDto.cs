using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.MOTs.Queries.GetMOTsForVehicleWithPagination
{
    public class ListedMOTDto : IMapFrom<MOT>
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MOT, ListedMOTDto>()
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.Date.Add(s.Duration)));
        }
    }
}
