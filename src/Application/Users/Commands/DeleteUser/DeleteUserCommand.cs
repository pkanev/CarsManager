using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Users.Commands.DeleteUser
{
    [Authorise(Roles = RoleConstants.ADMIN)]
    public class DeleteUserCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteUserCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Users.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(User), request.Id);

            entity.Roles.Clear();
            context.Users.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
