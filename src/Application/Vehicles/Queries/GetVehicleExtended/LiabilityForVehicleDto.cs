using System;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Vehicles.Queries.GetVehicleExtended
{
    public class LiabilityForVehicleDto : IMapFrom<Liability>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
