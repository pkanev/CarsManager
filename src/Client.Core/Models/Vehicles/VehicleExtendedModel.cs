using System.Collections.Generic;
using Client.Core.Dtos;
using Client.Core.Models.Employees;

namespace Client.Core.Models.Vehicles
{
    public class VehicleExtendedModel
    {
        public VehicleRecentLiabilitiesDto RecentLiabilities { get; set; }
        public IList<BasicEmployeeModel> Employees { get; set; } = new List<BasicEmployeeModel>();
    }
}
