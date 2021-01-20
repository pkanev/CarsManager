using System.Collections.Generic;
using CarsManager.Domain.Common;
using CarsManager.Domain.Enums;

namespace CarsManager.Domain.Entities
{
    public class Model : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public Make Make { get; set; }
        public int EngineDisplacement { get; set; }
        public FuelType Fuel { get; set; }
        public VehicleType VehicleType { get; set; }

        public ICollection<DomainEvent> DomainEvents { get; set; } = new HashSet<DomainEvent>();
    }
}
