using CarsManager.Domain.Common;
using CarsManager.Domain.Entities;

namespace CarsManager.Domain.Events
{
    public class ModelCreatedEvent : DomainEvent
    {
        public Model Model { get; }

        public ModelCreatedEvent(Model model)
        {
            Model = model;
        }
    }
}
