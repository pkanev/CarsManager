using System.Threading.Tasks;
using Client.Core.Models.Authentication;

namespace Client.Core.Services
{
    public interface IAuthService
    {
        public Task<AuthenticationResultModel> Login(string username, string password);
        public Task<AuthenticationResultModel> Register(RegisterModel model);
        public Task<AuthenticationResultModel> ChangePassword(PasswordChangeModel model);
        public void Logout();
    }
}
