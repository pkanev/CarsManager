using System;
using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReport
{
    public class LiabilityForReportDto : IMapFrom<Liability>
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LiabilityType { get; set; }
        public string LicencePlate { get; set; }
        public int MakeId { get; set; }
        public string Make { get; set; }
        public int ModelId { get; set; }
        public string Model { get; set; }
        public int VehicleType { get; set; }
        public string Color { get; set; }
        public int RemainingDays => (int)(EndDate - DateTime.Today).TotalDays;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Liability, LiabilityForReportDto>()
                .ForMember(d => d.LiabilityType, opt => opt.MapFrom(s => s.GetType().Name))
                .ForMember(d => d.Make, opt => opt.MapFrom(s => s.Vehicle.Model.Make.Name))
                .ForMember(d => d.MakeId, opt => opt.MapFrom(s => s.Vehicle.Model.Make.Id))
                .ForMember(d => d.Model, opt => opt.MapFrom(s => s.Vehicle.Model.Name))
                .ForMember(d => d.ModelId, opt => opt.MapFrom(s => s.Vehicle.Model.Id))
                .ForMember(d => d.LicencePlate, opt => opt.MapFrom(s => s.Vehicle.LicencePlate))
                .ForMember(d => d.Color, opt => opt.MapFrom(s => s.Vehicle.Color))
                .ForMember(d => d.VehicleType, opt => opt.MapFrom(s => (int)s.Vehicle.Model.VehicleType));
        }
    }
}
