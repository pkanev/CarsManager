using System.Collections.Generic;

namespace Client.Core.Models.Employees
{
    public class CreateEmployeeModel : EmployeeModel
    {
        public IList<int> VehicleIds { get; set; } = new List<int>();
    }
}
