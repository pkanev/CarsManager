using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.MOTs.Commands.CreateMOT
{
    public class CreateMOTCommand : IRequest<int>
    {
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public int DurationDays { get; set; }
    }

    public class CreateMOTCommandHandler : IRequestHandler<CreateMOTCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateMOTCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateMOTCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);

            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            var entity = new MOT
            {
                Vehicle = vehicle,
                Date = request.Date,
                Duration = TimeSpan.FromDays(request.DurationDays)
            };

            await context.MOTs.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
