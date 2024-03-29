﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models;
using Client.Core.Models.Repairs;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.Utils;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.RepairShops;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Repairs
{
    public class AddRepairViewModel : SubPageViewModel
    {
        private decimal vatRate = Properties.Settings.Default.VAT / 100;
        private readonly IVatService vatService;

        private ObservableCollection<RepairShopModel> repairShops;
        private RepairShopModel repairShop;
        private ObservableCollection<VehicleDto> vehicles;
        private VehicleDto vehicle;
        private RepairModel repair = new RepairModel { };
        private decimal price;

        public ObservableCollection<RepairShopModel> RepairShops
        {
            get => repairShops;
            set => SetProperty(ref repairShops, value);
        }

        public RepairShopModel RepairShop
        {
            get => repairShop;
            set
            {
                SetProperty(ref repairShop, value);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public ObservableCollection<VehicleDto> Vehicles
        {
            get => vehicles;
            set => SetProperty(ref vehicles, value);
        }

        public VehicleDto Vehicle
        {
            get => vehicle;
            set
            {
                SetProperty(ref vehicle, value);
                if (vehicle != null && repair != null)
                    repair.Mileage = Vehicle.Mileage;

                RaisePropertyChanged(() => CanSave);
                RaisePropertyChanged(() => IsVehicleLoaded);
                RaisePropertyChanged(() => Repair);
            }
        }

        public RepairModel Repair
        {
            get => repair;
            set => SetProperty(ref repair, value);
        }

        public decimal Price
        {
            get => price;
            set
            {
                SetProperty(ref price, value);
                RaisePropertyChanged(() => BasePrice);
                RaisePropertyChanged(() => Vat);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public string VatPercent => $"{Properties.Settings.Default.VAT}%";
        public decimal VatRate => vatRate;
        public string BasePrice => vatService.GetFormattedBasePrice(Price);
        public string Vat => vatService.GetFormattedVat(Price);

        public bool CanSave => RepairShop != null && Vehicle != null && Price > 0;
        public bool IsVehicleLoaded => Vehicle != null;

        public IMvxCommand AddRepairShopCommand { get; set; }
        public IMvxCommand EditRepairShopCommand { get; set; }
        public IMvxCommand DeleteRepairShopCommand { get; set; }
        public IMvxCommand SaveRepairCommand { get; set; }
        public IMvxCommand CancelCommand { get; set; }
        public IMvxCommand<string> RepairCheckedCommand { get; set; }

        public AddRepairViewModel(IVatService vatService, IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            this.vatService = vatService;

            AddRepairShopCommand = new MvxCommand(() => NavigationService.Navigate<RepairShopViewModel, NavigationModel<RepairShopModel>>(new NavigationModel<RepairShopModel> { Data = new RepairShopModel(), Callback = GetRepairShops }));
            EditRepairShopCommand = new MvxCommand(() => NavigationService.Navigate<RepairShopViewModel, NavigationModel<RepairShopModel>>(
                new NavigationModel<RepairShopModel>{
                    Data = RepairShop,
                    Callback = GetRepairShops
                }));
            DeleteRepairShopCommand = new MvxAsyncCommand(DeleteRepairShop);
            SaveRepairCommand = new MvxAsyncCommand(SaveRepair);
            CancelCommand = new MvxCommand(GoHome);
            RepairCheckedCommand = new MvxCommand<string>(RepairChecked);
        }

        public override async Task Initialize()
        {
            await GetRepairShops();
            await GetVehicles();
        }

        private async Task GetRepairShops()
        {
            var result = await ApiService.GetAsync<GetRepairShopsDto>("repairshops");

            if (result.IsSuccessStatusCode)
            {
                RepairShops = result.Content.RepairShops.ToObservableCollection();
                RepairShop = RepairShops.FirstOrDefault();
            }
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }

        private async Task GetVehicles()
        {
            var response = await ApiService.GetAsync<GetVehiclesDto>("vehicles");

            if (response.IsSuccessStatusCode)
            {
                Vehicles = response.Content.Vehicles.ToObservableCollection();
                Vehicle = Vehicles.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task DeleteRepairShop()
        {
            if (RepairShop == null)
                return;

            await ShowMessage($"Сигурни ли сте, че желаете да изтриете сервиз \"{RepairShop.Name}\" от базата? Само сервизи, в които няма направени ремонти могат да бъдат изтрити.", "Изтриване на сервиз", async () =>
            {
                var response = await ApiService.DeleteAsync<string>($"repairshops/{RepairShop.Id}");

                if (response.IsSuccessStatusCode)
                    await GetRepairShops();
                else
                    RaiseNotification(response.Error, "Грешка!!!");
            });
        }

        private async Task SaveRepair()
        {
            if (!CanSave)
                return;

            Repair.VehicleId = Vehicle.Id;
            Repair.RepairShopId = RepairShop.Id;
            Repair.FinalPrice = Price;

            var response = await ApiService.PostAsync<int>($"repairs", Repair);

            if (response.IsSuccessStatusCode)
                GoHome();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private void RepairChecked(string propertyName)
        {
            var property = Repair.GetType().GetProperty(propertyName);
            if (property == null)
                return;

            property.SetValue(Repair, !(bool)property.GetValue(Repair));
            RaisePropertyChanged(() => Repair);
        }
    }
}
