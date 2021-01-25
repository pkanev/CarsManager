using System;

namespace CarsManager.Domain.Entities
{
    public abstract class Liability
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; } = TimeSpan.FromDays(365);
    }
}
