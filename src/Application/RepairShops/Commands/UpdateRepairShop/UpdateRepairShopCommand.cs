using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.RepairShops.Commands.UpdateRepairShop
{
    public class UpdateRepairShopCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj) => Id == (obj as UpdateRepairShopCommand).Id;

        public override int GetHashCode() => base.GetHashCode();
    }

    public class UpdateRepairShopCommandHandler : IRequestHandler<UpdateRepairShopCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateRepairShopCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateRepairShopCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.RepairShops.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(RepairShop), request.Id);

            entity.Name = request.Name.Trim();

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
