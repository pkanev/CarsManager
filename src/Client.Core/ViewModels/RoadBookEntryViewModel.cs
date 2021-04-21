using System;
using System.Threading.Tasks;
using Client.Core.Models.RoadBook;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public abstract class RoadBookEntryViewModel : BaseViewModel<RoadBookNavigationModel>
    {
        private RoadBookBasicEntryModel model = new RoadBookBasicEntryModel();
        private Func<RoadBookBasicEntryModel, Task> onComplete;

        public RoadBookBasicEntryModel Model
        {
            get => model;
            set
            {
                SetProperty(ref model, value);
                RaisePropertyChanged(() => CanComplete);
            }
        }

        public abstract bool CanComplete { get; }

        public IMvxCommand CompleteCommand { get; set; }
        public IMvxCommand CancelCommand { get; set; }

        protected RoadBookEntryViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            CompleteCommand = new MvxAsyncCommand(Complete);
            CancelCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));
        }

        public override void Prepare(RoadBookNavigationModel parameter)
        {
            Model.Id = parameter.Id;
            onComplete = parameter.Callback;
        }

        public override async Task Initialize()
        {
            await GetRoadBookEntry();
        }

        private async Task GetRoadBookEntry()
        {
            if (Model.Id == 0)
                return;

            var response = await ApiService.GetAsync<RoadBookBasicEntryModel>($"roadbookentries/{Model.Id}");

            if (response.IsSuccessStatusCode)
            {
                Model = response.Content;
                OnRoadBookEntryFetched();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task Complete()
        {
            await onComplete(Model);
            await NavigationService.Close(this);
        }

        public virtual void OnRoadBookEntryFetched()
        {
            // no operation
        }
    }
}
