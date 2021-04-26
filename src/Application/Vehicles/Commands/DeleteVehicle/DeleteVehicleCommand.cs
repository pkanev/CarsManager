using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

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
            var entity = await context.Vehicles.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            if (!string.IsNullOrEmpty(entity.Image))
                imageManager.DeleteFile(request.ImagePath, entity.Image);

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
