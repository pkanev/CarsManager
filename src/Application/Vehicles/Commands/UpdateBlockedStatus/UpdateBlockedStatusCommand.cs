using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Commands.UpdateBlockedStatus
{
    [Authorise]
    public class UpdateBlockedStatusCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
    }

    public class UpdateBlockedStatusCommandHandler : IRequestHandler<UpdateBlockedStatusCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateBlockedStatusCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateBlockedStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles
                .Include(v => v.Employees)
                .FirstOrDefaultAsync(v => v.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            if (entity.Employees.Count > 0)
                throw new InvalidBlockVehicleException($"Vehicle {entity.LicencePlate} must not be assigned to employees");

            entity.IsBlocked = request.IsBlocked;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
