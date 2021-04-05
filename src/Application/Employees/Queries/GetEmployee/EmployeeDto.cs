using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Employees.Queries.GetEmployee
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string ImageName { get; set; }
        public string ImageAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDto>()
                .ForMember(d => d.Town, opt => opt.MapFrom(s => s.Town.Name))
                .ForMember(d => d.ImageName, opt => opt.MapFrom(s => s.Image));
        }
    }
}
