using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class AddMakeViewModel : BaseViewModel<NavigationModel>
    {
        private string make;
        private Func<Task> onAddComplete;

        public string Make
        {
            get => make;
            set
            {
                SetProperty(ref make, value);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(Make);

        public IMvxCommand CreateMakeCommand { get; set; }

        public AddMakeViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            CreateMakeCommand = new MvxAsyncCommand(CreateMake);
        }

        public override void Prepare(NavigationModel model)
        {
            onAddComplete = model.Callback;
        }

        private async Task CreateMake()
        {
            var result = await ApiService.PostAsync<int>("makes", new MakeModel { Name = Make });

            if (result.IsSuccessStatusCode)
            {
                await onAddComplete();
                await NavigationService.Close(this);
            }
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }
    }
}
