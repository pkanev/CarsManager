using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Towns.Commands.UpdateTown
{
    public class UpdateTownCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateTownCommandHandler : IRequestHandler<UpdateTownCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateTownCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateTownCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Towns.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Town), request.Id);

            entity.Name = request.Name.Trim();

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
