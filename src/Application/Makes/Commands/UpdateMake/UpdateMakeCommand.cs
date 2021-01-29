using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Makes.Commands.UpdateMake
{
    public class UpdateMakeCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj) => Id == (obj as UpdateMakeCommand).Id;

        public override int GetHashCode() => base.GetHashCode();
    }

    public class UpdateMakeCommandHandler : IRequestHandler<UpdateMakeCommand, Unit>
    {
        private readonly IApplicationDbContext context;

        public UpdateMakeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateMakeCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Makes.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Make), request.Id);

            entity.Name = request.Name.Trim();

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
