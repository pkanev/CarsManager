using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IApiService apiService;
        private MvxInteraction<NotificationBox> notificationInteraction = new MvxInteraction<NotificationBox>();

        public IMvxCommand LoginCommand { get; set; }
        public IMvxInteraction<NotificationBox> NotificationInteraction => notificationInteraction;


        public LoginViewModel(IMvxNavigationService navigationService, IApiService apiService)
        {
            this.navigationService = navigationService;
            this.apiService = apiService;
            LoginCommand = new MvxAsyncCommand(Login);
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            await Login();
        }

        public async Task Login()
        {
            var loginResponse = await apiService.GetAsync<string>("login");
            if (loginResponse.IsSuccessStatusCode)
                await navigationService.Navigate<HomeViewModel>();
            else
            {
                var request = new NotificationBox
                {
                    Message = $"Error: {loginResponse.Error}, Status: {loginResponse.StatusCode}",
                    Callback = () => { },
                };
                notificationInteraction.Raise(request);
            }
        }
    }
}
