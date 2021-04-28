using System.Threading.Tasks;
using Client.Core.Models.Authentication;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Account
{
    public class PasswordChangeViewModel : SubPageViewModel
    {
        private readonly IAuthService authService;

        private string username;

        public string Username => username;

        public IMvxCommand<(string, string)> ChangePasswordCommand { get; private set; }

        public PasswordChangeViewModel(
            IAuthService authService,
            IApiService apiService,
            IMvxNavigationService navigationService,
            ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            this.authService = authService;
            ChangePasswordCommand = new MvxAsyncCommand<(string, string)>(ChangePassword);
        }

        public override Task Initialize()
        {
            username = CurrentUserService.CurrentUser.Username;
            return base.Initialize();
        }

        private async Task ChangePassword((string, string) passwords)
        {
            if (string.IsNullOrWhiteSpace(passwords.Item1)
                || string.IsNullOrWhiteSpace(passwords.Item2))
                return;

            var result = await authService.ChangePassword(new PasswordChangeModel
            {
                Id = CurrentUserService.CurrentUser.Id,
                OldPassword = passwords.Item1,
                NewPassword = passwords.Item2,
            });

            if (result.IsSuccessfull)
                await NavigationService.Navigate<HomeViewModel>();
            else
                RaiseNotification(result.Message, "Грешка!!!");
        }
    }
}
