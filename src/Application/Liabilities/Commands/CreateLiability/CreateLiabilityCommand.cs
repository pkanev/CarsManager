using System;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Liabilities.Commands.CreateLiability
{
    [Authorise]
    public class CreateLiabilityCommand : IRequest<int>, IMapFrom<CreateLiabilityCommandDto>
    {
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LiabilityType Liability { get; set; }
    }

    public class CreateLiabilityCommandHandler : IRequestHandler<CreateLiabilityCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateLiabilityCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateLiabilityCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await context.Vehicles.FindAsync(request.VehicleId);

            if (vehicle == null)
                throw new NotFoundException(nameof(Vehicle), request.VehicleId);

            var entity = CreateLiability(request.Liability);

            entity.Vehicle = vehicle;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;

            await AddToContextAsync(entity, cancellationToken, request.Liability);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        private async Task AddToContextAsync(Liability entity, CancellationToken cancellationToken, LiabilityType liability)
        {
            switch (liability)
            {
                case LiabilityType.MOT:
                    await context.MOTs.AddAsync(entity as MOT, cancellationToken);
                    return;

                case LiabilityType.CivilLiability:
                    await context.CivilLiabilities.AddAsync(entity as CivilLiability, cancellationToken);
                    return;

                case LiabilityType.CarInsurance:
                    await context.CarInsurances.AddAsync(entity as CarInsurance, cancellationToken);
                    return;

                case LiabilityType.Vignette:
                    await context.Vignettes.AddAsync(entity as Vignette, cancellationToken);
                    return;

                default:
                    throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}"); ;
            }
        }

        private Liability CreateLiability(LiabilityType liability) => liability switch
        {
            LiabilityType.MOT => new MOT(),
            LiabilityType.CivilLiability => new CivilLiability(),
            LiabilityType.CarInsurance => new CarInsurance(),
            LiabilityType.Vignette => new Vignette(),
            _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}")
        };
    }
}
