using System.Threading.Tasks;
using Client.Core.Models.Authentication;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Account
{
    public class ProfileViewModel : SubPageViewModel
    {
        private readonly ICurrentUserService currentUserService;

        private string username;
        private string firstName;
        private string lastName;

        public string Username => username;

        public string FirstName
        {
            get => firstName;
            set
            {
                SetProperty(ref firstName, value);
                RaisePropertyChanged(() => CanUpdateUser);
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                SetProperty(ref lastName, value);
                RaisePropertyChanged(() => CanUpdateUser);
            }
        }

        public bool CanUpdateUser => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);

        public IMvxCommand UpdateUserCommand { get; private set; }

        public ProfileViewModel(ICurrentUserService currentUserService, IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            this.currentUserService = currentUserService;

            UpdateUserCommand = new MvxAsyncCommand(UpdateUser);
        }

        public override Task Initialize()
        {
            username = currentUserService.CurrentUser.Username;
            FirstName = currentUserService.CurrentUser.FirstName;
            LastName = currentUserService.CurrentUser.LastName;
            return base.Initialize();
        }

        private async Task UpdateUser()
        {
            var response = await ApiService.PutAsync<string>($"account/{currentUserService.CurrentUser.Id}", new UserModel
            {
                Id = currentUserService.CurrentUser.Id,
                FirstName = FirstName,
                LastName = lastName,
            });

            if (response.IsSuccessStatusCode)
            {
                currentUserService.CurrentUser.FirstName = FirstName;
                currentUserService.CurrentUser.LastName = LastName;

                GoHome();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
