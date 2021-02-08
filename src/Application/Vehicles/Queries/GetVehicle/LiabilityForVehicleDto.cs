using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehicle
{
    public class LiabilityForVehicleDto : IMapFrom<Liability>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Liability, LiabilityForVehicleDto>()
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Date))
                .ForPath(d => EndDate, opt => opt.MapFrom(s => s.Date.Add(s.Duration)));
        }
    }
}
