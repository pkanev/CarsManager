using Client.Core.Rest;
using Client.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Common
{
    public abstract class SubPageViewModel : BaseViewModel
    {
        public IMvxCommand GoHomeCommand { get; set; }
        
        protected SubPageViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            GoHomeCommand = new MvxCommand(GoHome);
        }

        protected void GoHome()
        {
            NavigationService.Navigate<HomeViewModel>();
        }
    }

    public abstract class SubPageViewModel<T> : BaseViewModel<T>
    {
        public IMvxCommand GoHomeCommand { get; set; }

        protected SubPageViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            GoHomeCommand = new MvxCommand(GoHome);
        }

        protected void GoHome()
        {
            NavigationService.Navigate<HomeViewModel>();
        }
    }
}
