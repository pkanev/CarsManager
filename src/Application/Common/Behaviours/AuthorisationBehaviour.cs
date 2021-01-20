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
        private readonly IIdentityService identityService;

        public AuthorisationBehaviour(
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            this.currentUserService = currentUserService;
            this.identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authoriseAttributes = request.GetType().GetCustomAttributes<AuthoriseAttribute>();

            if (authoriseAttributes.Any())
            {
                // Must be authenticated user
                if (currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authoriseAttributesWithRoles = authoriseAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authoriseAttributesWithRoles.Any())
                {
                    foreach (var roles in authoriseAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        var isAuthorised = false;
                        foreach (var role in roles)
                        {
                            var isInRole = await identityService.IsInRoleAsync(currentUserService.UserId, role.Trim());
                            if (isInRole)
                            {
                                isAuthorised = true;
                                break;
                            }
                        }

                        // Must be a member of at least one role in roles
                        if (!isAuthorised)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }

                // Policy-based authorisation
                var authoriseAttributesWithPolicies = authoriseAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authoriseAttributesWithPolicies.Any())
                {
                    foreach(var policy in authoriseAttributesWithPolicies.Select(a => a.Policy))
                    {
                        var isAuthorised = await identityService.AuthoriseAsync(currentUserService.UserId, policy);

                        if (!isAuthorised)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }
            }

            // User is authorised / authorisation not required
            return await next();
        }
    }
}
