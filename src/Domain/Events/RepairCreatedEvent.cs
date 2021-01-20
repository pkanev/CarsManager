using CarsManager.Domain.Common;
using CarsManager.Domain.Entities;

namespace CarsManager.Domain.Events
{
    public class RepairCreatedEvent : DomainEvent
    {
        public Repair Repair { get; }

        public RepairCreatedEvent(Repair repair)
        {
            Repair = repair;
        }
    }
}
