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

        public ProfileViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            UpdateUserCommand = new MvxAsyncCommand(UpdateUser);
        }

        public override Task Initialize()
        {
            username = CurrentUserService.CurrentUser.Username;
            FirstName = CurrentUserService.CurrentUser.FirstName;
            LastName = CurrentUserService.CurrentUser.LastName;
            return base.Initialize();
        }

        private async Task UpdateUser()
        {
            var response = await ApiService.PutAsync<string>($"account/{CurrentUserService.CurrentUser.Id}", new UserModel
            {
                Id = CurrentUserService.CurrentUser.Id,
                FirstName = FirstName,
                LastName = lastName,
            });

            if (response.IsSuccessStatusCode)
            {
                CurrentUserService.CurrentUser.FirstName = FirstName;
                CurrentUserService.CurrentUser.LastName = LastName;

                GoHome();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
