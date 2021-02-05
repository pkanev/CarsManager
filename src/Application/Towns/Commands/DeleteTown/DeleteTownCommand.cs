using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Towns.Commands.DeleteTown
{
    public class DeleteTownCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTownCommandHandler : IRequestHandler<DeleteTownCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteTownCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteTownCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Towns.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Town), request.Id);

            var hasEmployees = context.Employees
                .Any(e => e.TownId == request.Id);

            if (hasEmployees)
                throw new InvalidDeleteOperationException("Cannot delete a town with registered employees.");

            context.Towns.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
