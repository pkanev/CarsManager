using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Liabilities.Commands.UpdateLiability
{
    public class UpdateLiabilityCommand : IRequest, IMapFrom<UpdateLiabilityCommandDto>
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LiabilityType Liability { get; set; }
    }

    public class UpdateLiabilityCommandHandler : IRequestHandler<UpdateLiabilityCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly ILiabilityUtils liabilityUtils;

        public UpdateLiabilityCommandHandler(IApplicationDbContext context, ILiabilityUtils liabilityUtils)
        {
            this.context = context;
            this.liabilityUtils = liabilityUtils;
        }
        
        public async Task<Unit> Handle(UpdateLiabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await liabilityUtils.FindLiabilityAsync(request.Id, request.Liability, context);
            if (entity == null)
                throw new NotFoundException(liabilityUtils.GetLiabilityName(request.Liability), request.Id);

            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);

            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            entity.Vehicle = vehicle;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
