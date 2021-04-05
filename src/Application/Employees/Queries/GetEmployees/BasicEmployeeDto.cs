using AutoMapper;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Employees.Queries.GetEmployees
{
    public class BasicEmployeeDto : IMapFrom<Employee>
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string ImageName { get; set; }
        public string ImageAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, BasicEmployeeDto>()
                .ForMember(d => d.ImageName, opt => opt.MapFrom(s => s.Image));
        }
    }
}
