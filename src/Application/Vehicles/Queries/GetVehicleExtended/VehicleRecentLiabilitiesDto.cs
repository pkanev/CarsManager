using System.Linq;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehicleExtended
{
    public class VehicleRecentLiabilitiesDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public LiabilityForVehicleDto LastMOT { get; set; }
        public LiabilityForVehicleDto LastCivilLiability { get; set; }
        public LiabilityForVehicleDto LastCarInsurance { get; set; }
        public LiabilityForVehicleDto LastVignette { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleRecentLiabilitiesDto>()
                .ForMember(d => d.LastMOT, opt => opt.MapFrom(s => s.MOTs.OrderByDescending(m => m.StartDate).FirstOrDefault()))
                .ForMember(d => d.LastCivilLiability, opt => opt.MapFrom(s => s.CivilLiabilities.OrderByDescending(cl => cl.StartDate).FirstOrDefault()))
                .ForMember(d => d.LastCarInsurance, opt => opt.MapFrom(s => s.CarInsurances.OrderByDescending(ci => ci.StartDate).FirstOrDefault()))
                .ForMember(d => d.LastVignette, opt => opt.MapFrom(s => s.Vignettes.OrderByDescending(v => v.StartDate).FirstOrDefault()));
        }
    }
}
