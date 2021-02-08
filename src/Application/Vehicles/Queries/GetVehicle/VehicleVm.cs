using System.Collections.Generic;

namespace CarsManager.Application.Vehicles.Queries.GetVehicle
{
    public class VehicleVm
    {
        public VehicleDto Vehicle { get; set; }
        public IList<EmployeeForVehicleDto> Employees { get; set; }
        public IList<RepairForVehicleDto> Repairs { get; set; }
    }
}
