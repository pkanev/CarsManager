using System.Threading.Tasks;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Account
{
    public class LoginViewModel : EntryViewModel
    {
        private string username;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public IMvxCommand<string> LoginCommand { get; private set; }

        public LoginViewModel(IAuthService authService, IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(authService, apiService, navigationService, currentUserService)
        {
            LoginCommand = new MvxAsyncCommand<string>(Login);

        }

        public async Task Login(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
                return;

            var result = await AuthService.Login(Username, password);

            if (result.IsSuccessfull)
                await NavigationService.Navigate<HomeViewModel>();
            else
                RaiseNotification(result.Message, "Грешка!!!");
        }
    }
}
