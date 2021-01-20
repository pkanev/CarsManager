using CarsManager.Domain.Common;
using CarsManager.Domain.Entities;

namespace CarsManager.Domain.Events
{
    public class VehicleCreatedEvent : DomainEvent
    {
        public Vehicle Vehicle { get; }

        public VehicleCreatedEvent(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
