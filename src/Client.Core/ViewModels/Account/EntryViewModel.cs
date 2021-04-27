using System;
using System.Collections.Generic;
using System.Text;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.Settings;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels.Account
{
    public abstract class EntryViewModel : BaseViewModel
    {
        private readonly IAuthService authService;

        private MvxInteraction closeAppInteraction = new MvxInteraction();

        public IMvxCommand GoToServerSettingCommand { get; private set; }
        public IMvxCommand GoToLoginCommand { get; private set; }
        public IMvxCommand GoToRegisterCommand { get; private set; }
        public IMvxCommand CloseAppCommand { get; private set; }

        public IMvxInteraction CloseAppInteraction => closeAppInteraction;
        protected IAuthService AuthService => authService;

        protected EntryViewModel(IAuthService authService, IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            this.authService = authService;

            GoToServerSettingCommand = new MvxAsyncCommand(() => navigationService.Navigate<ServerSettingViewModel>());
            GoToLoginCommand = new MvxAsyncCommand(() => navigationService.Navigate<LoginViewModel>());
            GoToRegisterCommand = new MvxAsyncCommand(() => navigationService.Navigate<RegisterViewModel>());
            CloseAppCommand = new MvxCommand(() => closeAppInteraction.Raise());
        }
    }
}
