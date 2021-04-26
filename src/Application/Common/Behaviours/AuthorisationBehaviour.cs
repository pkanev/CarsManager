using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;

namespace CarsManager.Application.Common.Behaviours
{
    public class AuthorisationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IUserService userService;

        public AuthorisationBehaviour(
            ICurrentUserService currentUserService,
            IUserService userService)
        {
            this.currentUserService = currentUserService;
            this.userService = userService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authoriseAttributes = request.GetType().GetCustomAttributes<AuthoriseAttribute>();

            if (authoriseAttributes.Any())
            {
                // Must be authenticated user
                if (currentUserService.UserId == null)
                    throw new UnauthorizedAccessException();

                // Role-based authorization
                var authoriseAttributesWithRoles = authoriseAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authoriseAttributesWithRoles.Any())
                {
                    foreach (var roles in authoriseAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        var isAuthorised = false;
                        foreach (var role in roles)
                        {
                            var isInRole = await userService.IsInRoleAsync(currentUserService.UserId, role.Trim());
                            if (isInRole)
                            {
                                isAuthorised = true;
                                break;
                            }
                        }

                        // Must be a member of at least one role in roles
                        if (!isAuthorised)
                            throw new ForbiddenAccessException();
                    }
                }
            }

            // User is authorised / authorisation not required
            return await next();
        }
    }
}
