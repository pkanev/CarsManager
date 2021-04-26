using System.Threading.Tasks;
using Client.Core.Models.Authentication;
using Client.Core.Rest;

namespace Client.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiService apiService;
        private readonly ICurrentUserService currentUserService;

        public AuthService(IApiService apiService, ICurrentUserService currentUserService)
        {
            this.apiService = apiService;
            this.currentUserService = currentUserService;
        }

        public async Task<AuthenticationResultModel> Login(string username, string password)
        {
            var loginResponse = await apiService.PostAsync<AuthModel>("account/authenticate", new { Username = username, Password = password });
            if (!loginResponse.IsSuccessStatusCode)
                return new AuthenticationResultModel { IsSuccessfull = false, Message = loginResponse.Error };

            currentUserService.CurrentUser = loginResponse.Content;
            return new AuthenticationResultModel { IsSuccessfull = true };
        }

        public async Task<AuthenticationResultModel> Register(RegisterModel model)
        {
            var response = await apiService.PostAsync<AuthModel>("account/register", model);
            if (!response.IsSuccessStatusCode)
                return new AuthenticationResultModel { IsSuccessfull = false, Message = response.Error };

            currentUserService.CurrentUser = response.Content;
            return new AuthenticationResultModel { IsSuccessfull = true };
        }

        public async Task<AuthenticationResultModel> ChangePassword(PasswordChangeModel model)
        {
            var response = await apiService.PutAsync<AuthModel>($"account/{model.Id}/updatepassword", model);
            if (!response.IsSuccessStatusCode)
                return new AuthenticationResultModel { IsSuccessfull = false, Message = response.Error };

            currentUserService.CurrentUser = response.Content;
            return new AuthenticationResultModel { IsSuccessfull = true };
        }

        public void Logout()
        {
            currentUserService.CurrentUser = null;
        }
    }
}
