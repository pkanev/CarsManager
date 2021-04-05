using System.Collections.Generic;
using CarsManager.Application.Employees.Queries.GetEmployees;

namespace CarsManager.Application.Vehicles.Queries.GetVehicleExtended
{
    public class VehicleExtendedVm
    {
        public VehicleRecentLiabilitiesDto RecentLiabilities { get; set; }
        public IList<BasicEmployeeDto> Employees { get; set; }
    }
}
