using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (context.Users.Any(x => x.Username == request.Username.ToLower()))
                throw new IdentityException($"Username \"{request.Username}\" is already taken");

            byte[] passwordHash, passwordSalt;
            userService.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var user = new User();
            user.Id = Guid.NewGuid().ToString();
            user.Username = request.Username.ToLower().Trim();
            user.FirstName = request.FirstName.Trim();
            user.LastName = request.LastName.Trim();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (context.Users.Count() == 0)
            {
                UserRole adminRole = await context.UserRoles.FindAsync(RoleConstants.ADMIN);
                if (adminRole == null)
                    adminRole = new UserRole { Name = RoleConstants.ADMIN };

                user.Roles.Add(adminRole);
            }

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserDto>(user);
        }
    }
}
