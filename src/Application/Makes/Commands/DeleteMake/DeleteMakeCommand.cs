using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Makes.Commands.DeleteMake
{
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

            var models = await context.Models
                .Where(m => m.MakeId == request.Id)
                .ToListAsync();

            context.Models.RemoveRange(models);
            context.Makes.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
