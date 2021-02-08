using System.Collections.Generic;

namespace CarsManager.Application.Employees.Queries.GetEmployee
{
    public class EmployeeVm
    {
        public EmployeeDto Employee { get; set; }
        public IList<VehicleForEmployeeDto> Vehicles { get; set; }
    }
}
