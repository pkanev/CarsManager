using System.Threading.Tasks;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IUserService
    {
        //User Authenticate(string username, string password);
        //Task<IList<User>> GetAllAsync();
        //Task<User> CreateAsync(User user, string password);
        //Task UpdateAsync(User user, string password = null);
        //Task DeleteAsync(int id);
        //Task AssignRoleAsync(string userId, string role);
        Task<User> GetByIdAsync(string id);
        Task<string> GetUserNameAsync(string id);
        Task<bool> IsInRoleAsync(string userId, string role);
        bool IsInRoleAsync(User user, string role);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
