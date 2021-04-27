using Client.Core.Rest;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Settings
{
    public class ServerSettingViewModel : BaseViewModel
    {
        public string ServerAddress
        {
            get => Properties.Settings.Default.ApiAddress;
            set
            {
                Properties.Settings.Default.ApiAddress = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => ServerAddress);
            }
        }

        public IMvxCommand CloseCommand { get; private set; }

        public ServerSettingViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            CloseCommand = new MvxAsyncCommand(async() => await navigationService.Close(this));
        }
    }
}
