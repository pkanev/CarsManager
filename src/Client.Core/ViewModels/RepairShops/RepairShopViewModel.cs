﻿using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.RepairShops
{
    public class RepairShopViewModel : BaseViewModel<NavigationModel<RepairShopModel>>
    {
        private RepairShopModel repairShop = new RepairShopModel();
        private Func<Task> onSave;
        private bool isNewTown => repairShop?.Id == 0;

        public string Name
        {
            get => repairShop.Name;
            set
            {
                repairShop.Name = value;
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public string Caption => repairShop?.Id > 0
            ? "Редакция на сервиз"
            : "Добаввяне на сервиз";

        public bool CanSave => !string.IsNullOrWhiteSpace(repairShop.Name);

        public IMvxCommand SaveRepairShopCommand { get; private set; }
        public IMvxCommand CancelCommand { get; private set; }

        public RepairShopViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            SaveRepairShopCommand = new MvxAsyncCommand(SaveRepairShop);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<RepairShopModel> model)
        {
            repairShop = model.Data;
            onSave = model.Callback;
        }

        private async Task SaveRepairShop()
        {
            if (isNewTown)
                await InsertRepairShop();
            else
                await UpdateRepairShop();
        }

        private async Task InsertRepairShop()
        {
            var response = await ApiService.PostAsync<int>("repairshops", repairShop);

            if (response.IsSuccessStatusCode)
                await OnSuccess();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task UpdateRepairShop()
        {
            var response = await ApiService.PutAsync<string>($"repairshops?id={repairShop.Id}", repairShop);

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
