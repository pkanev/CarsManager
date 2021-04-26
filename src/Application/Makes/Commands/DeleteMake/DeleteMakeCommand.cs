using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Makes.Commands.DeleteMake
{
    [Authorise]
    public class DeleteMakeCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteMakeCommandHandler : IRequestHandler<DeleteMakeCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public DeleteMakeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteMakeCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Makes.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Make), request.Id);

            var hasModels = context.Models
                .Any(m => m.MakeId == request.Id);

            if (hasModels)
                throw new InvalidDeleteOperationException("Cannot delete a make with existing models.");

            context.Makes.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
