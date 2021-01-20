using CarsManager.Domain.Common;
using CarsManager.Domain.Entities;

namespace CarsManager.Domain.Events
{
    public class MakeCreatedEvent : DomainEvent
    {
        public Make Make { get; }

        public MakeCreatedEvent(Make make)
        {
            Make = make;
        }
    }
}
