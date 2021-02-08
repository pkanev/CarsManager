using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.MOTs.Commands.UpdateMOT
{
    public class UpdateMOTCommand : IRequest
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public int DurationDays { get; set; }
    }

    public class UpdateMOTCommandHandler : IRequestHandler<UpdateMOTCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateMOTCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateMOTCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.MOTs.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(MOT), request.Id);

            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);

            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            entity.Vehicle = vehicle;
            entity.Date = request.Date;
            entity.Duration = TimeSpan.FromDays(request.DurationDays);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
