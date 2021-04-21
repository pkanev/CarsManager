using System.Collections.Generic;
using Client.Core.Models.RoadBook;

namespace Client.Core.Models.Employees
{
    public class CreateEmployeeModel : EmployeeModel
    {
        public IList<int> VehicleIds { get; set; } = new List<int>();
        public IList<RoadBookBasicEntryModel> RoadBookEntries { get; set; } = new List<RoadBookBasicEntryModel>();
    }
}
