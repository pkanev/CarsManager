using Client.Core.Rest;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.RoadBook
{
    public class RoadBookEntryAddViewModel : RoadBookEntryViewModel
    {
        public override bool CanComplete => true;

        public RoadBookEntryAddViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
        }

    }
}
