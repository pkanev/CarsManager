using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Towns.Commands.CreateTown
{
    [Authorise]
    public class CreateTownCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateTownCommandHandler : IRequestHandler<CreateTownCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateTownCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateTownCommand request, CancellationToken cancellationToken)
        {
            var entity = new Town
            {
                Name = request.Name.Trim()
            };

            await context.Towns.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
