using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Models.MakesAndModels;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Makes
{
    public class DeleteMakeViewModel : BaseViewModel<NavigationModel<MakeModel>>
    {
        private MakeModel make;
        private Func<Task> onDeleteComplete;

        public MakeModel Make
        {
            get => make;
            set
            {
                SetProperty(ref make, value);
                RaisePropertyChanged(() => ConfirmationMessage);
            }
        }

        public string ConfirmationMessage => $"Сигруни ли сте, че желате да изтриете \"{Make.Name}\" от базата?";

        public IMvxCommand DeleteMakeCommand { get; set; }
        public IMvxCommand CancelCommand { get; set; }

        public DeleteMakeViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            DeleteMakeCommand = new MvxAsyncCommand(DeleteMake);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<MakeModel> model)
        {
            Make = model.Data ?? new MakeModel();
            onDeleteComplete = model.Callback;
        }

        private async Task DeleteMake()
        {
            var result = await ApiService.DeleteAsync<string>($"makes/{Make.Id}");

            if (result.IsSuccessStatusCode)
            {
                await onDeleteComplete();
                await NavigationService.Close(this);
                return;
            }

            RaiseNotification(result.Error, "Грешка!!!", () => NavigationService.Close(this));
        }
    }
}
