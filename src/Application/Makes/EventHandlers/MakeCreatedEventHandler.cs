using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Models;
using CarsManager.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsManager.Application.Makes.EventHandlers
{
    public class MakeCreatedEventHandler : INotificationHandler<DomainEventNotification<MakeCreatedEvent>>
    {
        private readonly ILogger<MakeCreatedEventHandler> logger;

        public MakeCreatedEventHandler(ILogger<MakeCreatedEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(DomainEventNotification<MakeCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            logger.LogInformation("CarsManager Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
