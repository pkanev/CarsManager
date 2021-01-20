using System.Collections.Generic;
using CarsManager.Domain.Common;

namespace CarsManager.Domain.Entities
{
    public class Make : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; } = new HashSet<Model>();
        public ICollection<DomainEvent> DomainEvents { get; set; } = new HashSet<DomainEvent>();
    }
}
