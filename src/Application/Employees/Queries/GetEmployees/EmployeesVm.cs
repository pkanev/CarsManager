using System.Collections.Generic;
using CarsManager.Application.Employees.Queries.Dtos;

namespace CarsManager.Application.Employees.Queries.GetEmployees
{
    public class EmployeesVm
    {
        public List<EmployeeDto> Employees { get; set; }
    }
}
