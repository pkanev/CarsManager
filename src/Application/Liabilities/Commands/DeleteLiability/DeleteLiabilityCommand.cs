using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Liabilities.Commands.DeleteLiability
{
    public class DeleteLiabilityCommand : IRequest
    {
        public int Id { get; set; }
        public LiabilityType Liability { get; set; }
    }

    public class DeleteLiabilityCommandHandler : IRequestHandler<DeleteLiabilityCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly ILiabilityUtils liabilityUtils;

        public DeleteLiabilityCommandHandler(IApplicationDbContext context, ILiabilityUtils liabilityUtils)
        {
            this.context = context;
            this.liabilityUtils = liabilityUtils;
        }

        public async Task<Unit> Handle(DeleteLiabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await liabilityUtils.FindLiabilityAsync(request.Id, request.Liability, context);
            if (entity == null)
                throw new NotFoundException(liabilityUtils.GetLiabilityName(request.Liability), request.Id);

            RemoveFromContext(entity, request.Liability);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private void RemoveFromContext(Liability entity, LiabilityType liability)
        {
            switch (liability)
            {
                case LiabilityType.MOT:
                    context.MOTs.Remove(entity as MOT);
                    return;

                case LiabilityType.CivilLiability:
                    context.CivilLiabilities.Remove(entity as CivilLiability);
                    return;

                case LiabilityType.CarInsurance:
                    context.CarInsurances.Remove(entity as CarInsurance);
                    return;

                case LiabilityType.Vignette:
                    context.Vignettes.Remove(entity as Vignette);
                    return;

                default:
                    throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}");
            }
        }
    }
}
