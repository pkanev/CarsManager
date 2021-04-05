using System.Collections.Generic;
using Client.Core.Models.Employees;
using Client.Core.Models.Vehicles;

namespace Client.Core.Dtos
{
    public class GetEmployeeDto
    {
        public EmployeeModel Employee { get; set; } = new EmployeeModel();
        public IList<BasicVehicleModel> Vehicles { get; set; } = new List<BasicVehicleModel>();
    }
}
