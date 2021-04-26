using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Users;
using CarsManager.Application.Users.Commands.AuthenticateUser;
using CarsManager.Application.Users.Commands.CreateUser;
using CarsManager.Application.Users.Commands.DeleteUser;
using CarsManager.Application.Users.Commands.UpdatePassword;
using CarsManager.Application.Users.Commands.UpdateUser;
using CarsManager.Application.Users.Queries.ChangeAdminStatus;
using CarsManager.Application.Users.Queries.GetUsers;
using CarsManager.Application.Users.Queries.GetUsersWithPagination;
using CarsManager.Infrastructure.Helpers;
using CarsManager.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarsManager.Server.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly AppSettings appSettings;

        public AccountController(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponseModel>> Authenticate(AuthenticateUserCommand command)
        {
            var user = await Mediator.Send(command);
            return Ok(CreateAuthenticateResponseModel(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticateResponseModel>> Register(CreateUserCommand command)
        {
            var user = await Mediator.Send(command);
            return Ok(CreateAuthenticateResponseModel(user));
        }

        [HttpPut("{id}/updatepassword")]
        public async Task<ActionResult<AuthenticateResponseModel>> UpdatePassword(string id, UpdatePasswordCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var user = await Mediator.Send(command);
            return Ok(CreateAuthenticateResponseModel(user));
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
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
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
