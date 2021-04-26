using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.RepairShops.Commands.CreateRepairShop
{
    [Authorise]
    public class CreateRepairShopCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateRepairShopCommandHandler : IRequestHandler<CreateRepairShopCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateRepairShopCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateRepairShopCommand request, CancellationToken cancellationToken)
        {
            var entity = new RepairShop
            {
                Name = request.Name.Trim()
            };

            await context.RepairShops.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
