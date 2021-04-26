using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<UserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public AuthenticateUserCommandHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<UserDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(x => x.Username == request.Username.ToLower());

            if (user == null)
                throw new IdentityException("Username or password is incorrect");

            if (!userService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new IdentityException("Username or password is incorrect");

            return mapper.Map<UserDto>(user);
        }
    }
}
