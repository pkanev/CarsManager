using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination
{
    public class ListedVehicleDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int Year { get; set; }
        public string LicencePlate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, ListedVehicleDto>()
                .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
                .ForMember(d => d.ModelId, opt => opt.MapFrom(s => s.Model.Id))
                .ForMember(d => d.MakeName, opt => opt.MapFrom(s => s.Model.Make.Name))
                .ForMember(d => d.MakeId, opt => opt.MapFrom(s => s.Model.Make.Id));
        }
    }
}
