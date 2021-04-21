using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Vehicles.Commands.UpdateMileage
{
    public class UpdateMileageCommand : IRequest
    {
        public int VehicleId { get; set; }
        public int Mileage { get; set; }
    }

    public class UpdateMileageCommandHandler : IRequestHandler<UpdateMileageCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateMileageCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateMileageCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles.FindAsync(request.VehicleId);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            entity.Mileage = request.Mileage;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
