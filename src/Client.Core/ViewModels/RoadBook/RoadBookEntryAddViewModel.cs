using Client.Core.Rest;
using Client.Core.Services;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.RoadBook
{
    public class RoadBookEntryAddViewModel : RoadBookEntryViewModel
    {
        public override bool CanComplete => true;

        public RoadBookEntryAddViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
        }

    }
}
