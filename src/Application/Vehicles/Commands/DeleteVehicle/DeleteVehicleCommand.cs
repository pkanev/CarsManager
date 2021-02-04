using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public DeleteVehicleCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            entity.Employees.Clear();
            entity.Repairs.Clear();
            entity.MOTs.Clear();
            entity.CivilLiabilities.Clear();
            entity.CarInsurances.Clear();
            entity.Vignettes.Clear();
            context.Vehicles.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
