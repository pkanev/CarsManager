using System.Threading.Tasks;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService authService;
        private string username;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public IMvxCommand<string> LoginCommand { get; private set; }
        public IMvxCommand GoToRegisterCommand { get; private set; }

        public LoginViewModel(IAuthService authService, IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            this.authService = authService;
            LoginCommand = new MvxAsyncCommand<string>(Login);
            GoToRegisterCommand = new MvxAsyncCommand(() => navigationService.Navigate<RegisterViewModel>());
        }

        public async Task Login(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
                return;

            var result = await authService.Login(Username, password);

            if (result.IsSuccessfull)
                await NavigationService.Navigate<HomeViewModel>();
            else
                RaiseNotification(result.Message, "Грешка!!!");
        }
    }
}
