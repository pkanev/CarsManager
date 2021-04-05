using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class EditMakeViewModel : BaseViewModel<NavigationModel<MakeModel>>
    {
        private MakeModel make;
        private Func<Task> onEditComplete;

        public MakeModel Make
        {
            get => make;
            set
            {
                SetProperty(ref make, value);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(Make.Name);

        public IMvxCommand EditMakeCommand { get; set; }

        public EditMakeViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            EditMakeCommand = new MvxAsyncCommand(EditMake);
        }

        public override void Prepare(NavigationModel<MakeModel> model)
        {
            Make = model.Data ?? new MakeModel();
            onEditComplete = model.Callback;
        }

        private async Task EditMake()
        {
            var result = await ApiService.PutAsync<string>($"makes?id={Make.Id}", Make);

            if (result.IsSuccessStatusCode)
            {
                await onEditComplete();
                await NavigationService.Close(this);
            }
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }
    }
}
