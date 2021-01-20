using CarsManager.Domain.Common;
using CarsManager.Domain.Entities;

namespace CarsManager.Domain.Events
{
    public class EmployeeCreatedEvent : DomainEvent
    {
        public Employee Employee { get; }

        public EmployeeCreatedEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}
