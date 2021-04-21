using System;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.RoadBookEntries.Queries
{
    public class RoadBookBasicEntryDto : IMapFrom<RoadBookEntry>
    {
        public int Id { get; set; }
        public DateTime CheckedOut { get; set; } = DateTime.Now;
        public DateTime? CheckedIn { get; set; }
        public int VehicleId { get; set; }
        public int OldMileage { get; set; }
        public int? NewMileage { get; set; }
        public string Destination { get; set; }
    }
}
