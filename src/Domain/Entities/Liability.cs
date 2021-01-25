using System;
using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public abstract class Liability : AuditableEntity
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; } = TimeSpan.FromDays(365);
    }
}
