using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Users.Commands.UpdateUser
{
    [Authorise]
    public class UpdateUserCommand : IRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Users.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(User), request.Id);

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
