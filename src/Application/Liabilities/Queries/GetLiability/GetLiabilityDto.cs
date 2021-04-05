using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Liabilities.Queries.GetLiability
{
    public class GetLiabilityDto : IMapFrom<Liability>
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string LicencePlate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LiabilityType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Liability, GetLiabilityDto>()
                .ForMember(d => d.LicencePlate, opt => opt.MapFrom(s => s.Vehicle.LicencePlate));
        }
    }
}
