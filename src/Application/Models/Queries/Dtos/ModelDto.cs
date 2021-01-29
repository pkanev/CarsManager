using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Models.Queries.Dtos
{
    public class ModelDto : IMapFrom<Model>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string Make { get; set; }
        public int VehicleType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Model, ModelDto>()
                .ForMember(d => d.VehicleType, opt => opt.MapFrom(s => (int)s.VehicleType))
                .ForMember(d => d.Make, opt => opt.MapFrom(s => s.Make.Name));
        }
    }
}
