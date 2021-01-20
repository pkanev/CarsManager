using System.Collections.Generic;
using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public class RepairShop : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<DomainEvent> DomainEvents { get; set; } = new HashSet<DomainEvent>();
    }
}
