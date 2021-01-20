using System;
using System.Collections.Generic;
using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public abstract class Liability : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; } = TimeSpan.FromDays(365);

        public ICollection<DomainEvent> DomainEvents { get; set; } = new HashSet<DomainEvent>();
    }
}
