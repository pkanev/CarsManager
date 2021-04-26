using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Repairs.Commands.DeleteRepair
{
    [Authorise]
    public class DeleteRepairCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteRepairCommandHandler : IRequestHandler<DeleteRepairCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteRepairCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteRepairCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Repairs.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Repair), request.Id);

            context.Repairs.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
