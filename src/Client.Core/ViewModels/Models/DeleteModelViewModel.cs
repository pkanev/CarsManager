using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Models
{
    public class DeleteModelViewModel : BaseViewModel<NavigationModel<ModelModel>>
    {
        private ModelModel model;
        private Func<Task> onDeleteComplete;

        public ModelModel Model
        {
            get => model;
            set
            {
                SetProperty(ref model, value);
                RaisePropertyChanged(() => ConfirmationMessage);
            }
        }

        public string ConfirmationMessage => $"Сигруни ли сте, че желате да изтриете \"{Model.Name}\" от базата?";

        public IMvxCommand DeleteModelCommand { get; set; }
        public IMvxCommand CancelCommand { get; set; }

        public DeleteModelViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            DeleteModelCommand = new MvxAsyncCommand(DeleteModel);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<ModelModel> model)
        {
            Model = model.Data;
            onDeleteComplete = model.Callback;
        }

        private async Task DeleteModel()
        {
            var result = await ApiService.DeleteAsync<string>($"models/{Model.Id}");

            if (result.IsSuccessStatusCode)
            {
                await onDeleteComplete();
                await NavigationService.Close(this);
            }
            else
                RaiseNotification(result.Error, "Грешка!!!", () => NavigationService.Close(this));
        }
    }
}