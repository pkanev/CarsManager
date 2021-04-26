using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.RepairShops.Commands.DeleteRepairShop
{
    [Authorise]
    public class DeleteRepairShopCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteRepairShopCommandHandler : IRequestHandler<DeleteRepairShopCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteRepairShopCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteRepairShopCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.RepairShops.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(RepairShop), request.Id);

            var hasRepairs = context.Repairs
                .Any(r => r.RepairShopId == request.Id);

            if (hasRepairs)
                throw new InvalidDeleteOperationException("Cannot delete a repair shop which has made repairs.");

            context.RepairShops.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
