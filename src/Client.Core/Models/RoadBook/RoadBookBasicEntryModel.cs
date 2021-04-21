using System;

namespace Client.Core.Models.RoadBook
{
    public class RoadBookBasicEntryModel
    {
        public int Id { get; set; }
        public DateTime CheckedOut { get; set; } = DateTime.Now;
        public DateTime? CheckedIn { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public int OldMileage { get; set; }
        public int? NewMileage { get; set; }
        public string Destination { get; set; }
    }
}
