using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Models.Commands.DeleteModel
{
    public class DeleteModelCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public DeleteModelCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Makes.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Model), request.Id);

            var hasVehicles = context.Vehicles
                .Any(v => v.ModelId == request.Id);

            if (hasVehicles)
                throw new InvalidDeleteOperationException("Cannot delete a model with existing vehicles.");

            context.Makes.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
