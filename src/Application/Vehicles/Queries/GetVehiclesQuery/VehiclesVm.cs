using System.Collections.Generic;
using CarsManager.Application.Vehicles.Queries.GetVehicle;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesQuery
{
    public class VehiclesVm
    {
        public IList<VehicleDto> Vehicles { get; set; }
    }
}
