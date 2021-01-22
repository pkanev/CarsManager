using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Models;
using CarsManager.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsManager.Application.Vehicles.Eventhandlers
{
    public class VehicleCreatedEventHandler : INotificationHandler<DomainEventNotification<VehicleCreatedEvent>>
    {
        private readonly ILogger<VehicleCreatedEventHandler> logger;

        public VehicleCreatedEventHandler(ILogger<VehicleCreatedEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(DomainEventNotification<VehicleCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            logger.LogInformation("CarsManager Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
