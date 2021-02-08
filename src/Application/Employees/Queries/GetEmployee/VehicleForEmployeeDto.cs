using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Employees.Queries.GetEmployee
{
    public class VehicleForEmployeeDto : IMapFrom<Vehicle>
    {
        public int Id { get; set; }
        public string LicencePlate { get; set; }
    }
}
