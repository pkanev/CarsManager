using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Users;
using CarsManager.Application.Users.Commands.DeleteUser;
using CarsManager.Application.Users.Commands.UpdateUser;
using CarsManager.Application.Users.Queries.ChangeAdminStatus;
using CarsManager.Application.Users.Queries.GetUsers;
using CarsManager.Application.Users.Queries.GetUsersWithPagination;
using CarsManager.Domain.Entities;
using CarsManager.Infrastructure.Utils;
using CarsManager.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarsManager.Server.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IApplicationDbContext context;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AccountController(IApplicationDbContext context, IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet()]
        public async Task<IList<UserDto>> GetUsersWithPagination()
            => await Mediator.Send(new GetUsersQuery());

        [HttpGet("pages")]
        public async Task<ActionResult<PaginatedList<UserDto>>> GetUsersWithPagination([FromQuery] GetUsersWithPaginationQuery query)
            => await Mediator.Send(query);

        [HttpPost("{id}/adminstatus")]
        public async Task<ActionResult> ChangeAdminStatus(string id, ChangeAdminStatusCommand command)
        {
            if (id != command.UserId)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });

            return NoContent();
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponseModel>> Authenticate(AuthenticateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(x => x.Username == model.Username.Trim().ToLower());

            if (user == null)
                throw new IdentityException("Username or password is incorrect");

            if (!userService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                throw new IdentityException("Username or password is incorrect");

            return Ok(CreateAuthenticateResponseModel(mapper.Map<UserDto>(user)));
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticateResponseModel>> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (context.Users.Any(x => x.Username == model.Username.Trim().ToLower()))
                throw new IdentityException($"Username \"{model.Username}\" is already taken");

            byte[] passwordHash, passwordSalt;
            userService.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            var user = new User();
            user.Id = Guid.NewGuid().ToString();
            user.Username = model.Username.ToLower().Trim();
            user.FirstName = model.FirstName.Trim();
            user.LastName = model.LastName.Trim();
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
            await context.SaveChangesAsync(new CancellationToken());

            return Ok(CreateAuthenticateResponseModel(mapper.Map<UserDto>(user)));
        }

        [Authorize]
        [HttpPut("{id}/updatepassword")]
        public async Task<ActionResult<AuthenticateResponseModel>> UpdatePassword(string id, UpdatePasswordModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
                return BadRequest();

            var user = await userService.GetByIdAsync(model.Id);
            if (user == null)
                throw new NotFoundException(nameof(User), model.Id);

            if (!userService.VerifyPasswordHash(model.OldPassword, user.PasswordHash, user.PasswordSalt))
                throw new IdentityException("Incorrect password!");

            byte[] passwordHash, passwordSalt;
            userService.CreatePasswordHash(model.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await context.SaveChangesAsync(new CancellationToken());

            return Ok(CreateAuthenticateResponseModel(mapper.Map<UserDto>(user)));
        }

        private AuthenticateResponseModel CreateAuthenticateResponseModel(UserDto user)
            => new AuthenticateResponseModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = user.Roles,
                IsAdmin = user.IsAdmin,
                Token = CreateToken(user),
            };

        private string CreateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration[Constants.JWT_KEY]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
            };

            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
