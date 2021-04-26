using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Users.Commands.UpdatePassword
{
    [Authorise]
    public class UpdatePasswordCommand : IRequest<UserDto>
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class UpdatePasswordCommandHanlder : IRequestHandler<UpdatePasswordCommand, UserDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UpdatePasswordCommandHanlder(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<UserDto> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetByIdAsync(request.Id);
            if (user == null)
                throw new NotFoundException(nameof(User), request.Id);

            if (!userService.VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt))
                throw new IdentityException("Incorrect password!");

            byte[] passwordHash, passwordSalt;
            userService.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDto>(user);
        }
    }
}
