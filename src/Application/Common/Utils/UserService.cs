using System;
using System.Linq;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Common.Utils
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext context;

        public UserService(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                return false;

            return user.Roles.Any(r => r.Name == role);
        }

        public bool IsInRoleAsync(User user, string role)
        {
            if (user == null)
                return false;

            return user.Roles.Any(r => r.Name == role);
        }

        public async Task<string> GetUserNameAsync(string id)
        {
            var user = await context.Users.FindAsync(id);
            return user?.Username;
        }

        public async Task<User> GetByIdAsync(string id)
            => await context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (storedHash.Length != 64) return false;
            if (storedSalt.Length != 128) return false;

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != storedHash[i]) return false;
            }

            return true;
        }
    }
}
