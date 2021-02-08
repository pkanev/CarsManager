using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.MOTs.Queries.GetMOT
{
    public class MOTDto : IMapFrom<MOT>
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string LicencePlate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MOT, MOTDto>()
                .ForMember(d => d.LicencePlate, opt => opt.MapFrom(s => s.Vehicle.LicencePlate))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.Date.Add(s.Duration)));
        }
    }
}
