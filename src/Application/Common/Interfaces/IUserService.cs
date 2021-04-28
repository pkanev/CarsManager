using System.Threading.Tasks;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(string id);
        Task<string> GetUserNameAsync(string id);
        Task<bool> IsInRoleAsync(string userId, string role);
        bool IsInRoleAsync(User user, string role);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
