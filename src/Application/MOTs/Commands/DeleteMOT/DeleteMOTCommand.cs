using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.MOTs.Commands.DeleteMOT
{
    public class DeleteMOTCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteMOTCommandHandler : IRequestHandler<DeleteMOTCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteMOTCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteMOTCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.MOTs.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(MOT), request.Id);

            context.MOTs.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
