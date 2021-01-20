using CarsManager.Domain.Common;
using System.Threading.Tasks;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
