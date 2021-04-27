using System.Threading.Tasks;
using Client.Core.Models.Authentication;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Account
{
    public class RegisterViewModel : EntryViewModel
    {
        private string firstName;
        private string lastName;
        private string username;

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public IMvxCommand<string> RegisterCommand { get; private set; }

        public RegisterViewModel(IAuthService authService, IApiService apiService, IMvxNavigationService navigationService)
            : base(authService, apiService, navigationService)
        {
            RegisterCommand = new MvxAsyncCommand<string>(Register);
        }

        private async Task Register(string password)
        {
            if (string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName)
                || string.IsNullOrWhiteSpace(password))
                return;

            var result = await AuthService.Register(new RegisterModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Password = password,
            });

            if (result.IsSuccessfull)
                await NavigationService.Navigate<HomeViewModel>();
            else
                RaiseNotification(result.Message, "Грешка!!!");
        }
    }
}
