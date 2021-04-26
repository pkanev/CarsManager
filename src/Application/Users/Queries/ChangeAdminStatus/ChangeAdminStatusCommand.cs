using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Users.Queries.ChangeAdminStatus
{
    [Authorise(Roles = RoleConstants.ADMIN)]
    public class ChangeAdminStatusCommand : IRequest
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class ChangeAdminStatusCommandHandler : IRequestHandler<ChangeAdminStatusCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserService userService;

        public ChangeAdminStatusCommandHandler(IApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<Unit> Handle(ChangeAdminStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetByIdAsync(request.UserId);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            if (userService.IsInRoleAsync(user, RoleConstants.ADMIN) == request.IsAdmin)
                return Unit.Value;

            var adminRole = await context.UserRoles.FindAsync(RoleConstants.ADMIN);

            if (request.IsAdmin)
                user.Roles.Add(adminRole);
            else
                user.Roles.Remove(adminRole);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
