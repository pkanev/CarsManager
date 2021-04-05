using System;

namespace CarsManager.Domain.Entities
{
    public abstract class Liability
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;
    }
}
