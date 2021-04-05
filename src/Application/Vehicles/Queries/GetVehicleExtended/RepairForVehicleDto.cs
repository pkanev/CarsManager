using System;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehicleExtended
{
    public class RepairForVehicleDto : IMapFrom<Repair>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
