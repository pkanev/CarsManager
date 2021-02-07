using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeDto
    {
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public IFormFile Photo { get; set; }
    }
}
