using System;
using System.Collections.Generic;

namespace CarsManager.Domain.Entities
{
    public class RoadBookEntry
    {
        public int Id { get; set; }
        public DateTime CheckedOut { get; set; } = DateTime.Now;
        public DateTime? CheckedIn { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int OldMileage { get; set; }
        public int? NewMileage { get; set; }
        public string Destination { get; set; }
        public ICollection<Employee> Employees { get; private set; } = new HashSet<Employee>();
        public ICollection<Employee> ActiveUsers { get; private set; } = new HashSet<Employee>();

        public override string ToString()
            => $"Id: {Id}, CheckedOut: {CheckedOut}, CheckedIn: {CheckedIn}, OldMileage: {OldMileage}, NewMileage: {NewMileage}, Destination: {Destination}";
    }
}
