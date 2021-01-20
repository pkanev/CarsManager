using CarsManager.Application.Common.Models;
using System.Threading.Tasks;

namespace CarsManager.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthoriseAsync(string userId, string policyName);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId);
    }
}
