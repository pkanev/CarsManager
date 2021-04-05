using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Employees.Queries.GetEmployee
{
    public class VehicleForEmployeeDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public string LicencePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleForEmployeeDto>()
                .ForMember(d => d.Make, opt => opt.MapFrom(s => s.Model.Make.Name))
                .ForMember(d => d.Model, opt => opt.MapFrom(s => s.Model.Name));
        }
    }
}
