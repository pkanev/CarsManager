using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Towns
{
    public class TownViewModel : BaseViewModel<NavigationModel<TownModel>>
    {
        private TownModel town;
        private Func<Task> onSave;
        private bool isNewTown => town?.Id == 0;

        public string Name
        {
            get => town.Name;
            set
            {
                town.Name = value;
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public string Caption => town?.Id > 0
            ? "Редакция на град"
            : "Добаввяне на град";

        public bool CanSave => !string.IsNullOrWhiteSpace(town.Name);

        public IMvxCommand SaveTownCommand { get; set; }
        public IMvxCommand CancelCommand { get; set; }

        public TownViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            SaveTownCommand = new MvxAsyncCommand(SaveTown);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<TownModel> model)
        {
            town = model.Data;
            onSave = model.Callback;
        }

        private async Task SaveTown()
        {
            if (isNewTown)
                await InsertTown();
            else
                await UpdateTown();
        }

        private async Task InsertTown()
        {
            var response = await ApiService.PostAsync<int>("towns", town);

            if (response.IsSuccessStatusCode)
                await OnSuccess();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task UpdateTown()
        {
            var response = await ApiService.PutAsync<string>($"towns?id={town.Id}", town);

            if (response.IsSuccessStatusCode)
                await OnSuccess();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task OnSuccess()
        {
            await onSave();
            await NavigationService.Close(this);
        }
    }
}
