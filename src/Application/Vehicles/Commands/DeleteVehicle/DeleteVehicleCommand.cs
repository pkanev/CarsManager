using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Commands.DeleteVehicle
{
    [Authorise]
    public class DeleteVehicleCommand : IRequest
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
    }

    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Unit>
    {
        private readonly IApplicationDbContext context;
        private readonly IImageManager imageManager;

        public DeleteVehicleCommandHandler(IApplicationDbContext context, IImageManager imageManager)
        {
            this.context = context;
            this.imageManager = imageManager;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles
                .Include(v => v.RoadBookEntries)
                .FirstOrDefaultAsync(v => v.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            if (entity.RoadBookEntries.Any(r => r.ActiveUsers.Count > 0))
                throw new ForbiddenVehicleDeletionException($"Vehicle {entity.LicencePlate} has assigned employees.");

            if (!string.IsNullOrEmpty(entity.Image))
                imageManager.DeleteFile(request.ImagePath, entity.Image);

            entity.Employees.Clear();
            entity.Repairs.Clear();
            entity.MOTs.Clear();
            entity.CivilLiabilities.Clear();
            entity.CarInsurances.Clear();
            entity.Vignettes.Clear();
            entity.RoadBookEntries.Clear();
            context.Vehicles.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
