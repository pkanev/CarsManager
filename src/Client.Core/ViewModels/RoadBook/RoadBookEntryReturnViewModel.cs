using System;
using Client.Core.Rest;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.RoadBook
{
    public class RoadBookEntryReturnViewModel : RoadBookEntryViewModel
    {
        public string Destination
        {
            get => Model.Destination;
            set
            {
                Model.Destination = value;
                RaisePropertyChanged(() => Destination);
                RaisePropertyChanged(() => CanComplete);
            }
        }

        public int? NewMileage
        {
            get => Model.NewMileage;
            set
            {
                Model.NewMileage = value;
                RaisePropertyChanged(() => NewMileage);
                RaisePropertyChanged(() => CanComplete);
            }
        }

        public DateTime? CheckedIn
        {
            get => Model.CheckedIn;
            set
            {
                Model.CheckedIn = value;
                RaisePropertyChanged(() => CheckedIn);
                RaisePropertyChanged(() => CanComplete);
            }
        }

        public override bool CanComplete
            => Model.CheckedIn.GetValueOrDefault() >= Model.CheckedOut
            && Model.NewMileage.GetValueOrDefault() >= Model.OldMileage
            && !string.IsNullOrWhiteSpace(Model.Destination);

        public RoadBookEntryReturnViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
        }

        public override void OnRoadBookEntryFetched()
        {
            RaisePropertyChanged(() => Destination);
            RaisePropertyChanged(() => NewMileage);
            RaisePropertyChanged(() => CheckedIn);
        }
    }
}
