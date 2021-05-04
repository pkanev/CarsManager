using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Dtos;
using Client.Core.Models;
using Client.Core.Models.Employees;
using Client.Core.Models.Liabilities;
using Client.Core.Models.Repairs;
using Client.Core.Models.RoadBook;
using Client.Core.Models.Vehicles;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.Utils;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.Liabilities;
using Client.Core.ViewModels.RoadBook;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels.Vehicles
{
    public class EditVehicleViewModel : SubPageViewModel
    {
        private decimal vatRate = Properties.Settings.Default.VAT / 100;

        private ObservableCollection<VehicleDto> vehicles;
        private VehicleDto vehicle;
        private VehicleRecentLiabilitiesDto recentLiabilities;
        private MakeModel make;
        private ObservableCollection<MakeModel> makes;
        private ModelModel model;
        private ObservableCollection<ModelModel> models;
        private FuelTypeModel fuelType;
        private VehicleTypeModel vehicleType;
        private string licencePlate;
        private string color;
        private string image;
        private bool isBlocked;
        private bool isImageUpdated;
        private int selectedTab;
        private MvxInteraction<UploadInteractionHandler> uploadInteraction = new MvxInteraction<UploadInteractionHandler>();
        private ObservableCollection<LiabilityExtendedModel> mots;
        private ObservableCollection<LiabilityExtendedModel> civilliabilities;
        private ObservableCollection<LiabilityExtendedModel> carInsurances;
        private ObservableCollection<LiabilityExtendedModel> vignettes;
        private ObservableCollection<RepairModel> repairs;
        private RepairModel selectedRepair;
        private ObservableCollection<RepairShopModel> repairShops;
        private RepairShopModel selectedRepairShop;
        private ObservableCollection<BasicEmployeeModel> employeesForVehicle;
        private BasicEmployeeModel assignedEmployee;
        private ObservableCollection<BasicEmployeeModel> allEmployees;
        private BasicEmployeeModel nonAssignedEmployee;
        private readonly IVatService vatService;

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
                if (value == null || value?.Id == vehicle?.Id)
                    return;

                SetProperty(ref vehicle, value);

                if (vehicle.Id != RecentLiabilities?.Id)
                    Task.Run(() => GetVehicleExtended());

                VehicleType = VehicleTypes[vehicle.VehicleType];
                FuelType = FuelTypes[vehicle.Fuel];
                Color = Vehicle.Color;
                ImageAddress = vehicle.ImageAddress;
                LicencePlate = vehicle.LicencePlate;
                IsBlocked = vehicle.IsBlocked;

                if (Makes != null && Make?.Id != vehicle.MakeId)
                    Make = Makes.FirstOrDefault(m => m.Id == vehicle.MakeId);

                Task.Run(GetLiabilities);
                Task.Run(GetRepairs);

                RaisePropertyChanged(() => IsValid);
            }
        }

        public VehicleRecentLiabilitiesDto RecentLiabilities
        {
            get => recentLiabilities;
            set => SetProperty(ref recentLiabilities, value);
        }

        public ObservableCollection<MakeModel> Makes
        {
            get => makes;
            set => SetProperty(ref makes, value);
        }

        public MakeModel Make
        {
            get => make;
            set
            {
                SetProperty(ref make, value);
                Task.Run(async () => await GetModels());
            }
        }

        public ObservableCollection<ModelModel> Models
        {
            get => models;
            set
            {
                SetProperty(ref models, value);
                RaisePropertyChanged(() => CanSelectModel);
                Model = Models.FirstOrDefault(m => m.Id == Vehicle.ModelId);
                if (Model == null)
                    Model = Models.FirstOrDefault();
            }
        }

        public ModelModel Model
        {
            get => model;
            set
            {
                SetProperty(ref model, value);
                RaisePropertyChanged(() => IsVehicleLoaded);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        public string LicencePlate
        {
            get => licencePlate;
            set
            {
                SetProperty(ref licencePlate, value);
                RaisePropertyChanged(() => IsValid);
            }
        }
        public bool IsBlocked
        {
            get => isBlocked;
            set => SetProperty(ref isBlocked, value);
        }

        public string ImageAddress
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public FuelTypeModel FuelType
        {
            get => fuelType;
            set => SetProperty(ref fuelType, value);
        }

        public VehicleTypeModel VehicleType
        {
            get => vehicleType;
            set
            {
                SetProperty(ref vehicleType, value);
                Task.Run(() => GetModels());
            }
        }

        public bool IsVehicleLoaded => Vehicle != null;
        public bool CanSelectModel => Models?.Count > 0;
        public bool HasSelectedRepair => SelectedRepair != null;
        public bool CanSaveRepair => SelectedRepairShop != null && Vehicle != null && SelectedRepair != null && RepairPrice > 0;

        public bool IsValid => Model != null
            && Regex.IsMatch(LicencePlate.ToUpper(), @"^[ETYOPAHKXCBMВЕРТОАСХКНМ]{1,2}[0-9]{4}[ETYOPAHKXCBMВЕРТОАСХКНМ]{1,2}$");

        public bool CanAssignEmployee =>
            Vehicle != null
            && !Vehicle.IsBlocked
            && NonAssignedEmployee != null
            && NonAssignedEmployee.IsEmployed
            && !EmployeesForVehicle.Any(e => e.Id == NonAssignedEmployee.Id);

        public ObservableCollection<LiabilityExtendedModel> MOTs
        {
            get => mots;
            set => SetProperty(ref mots, value);
        }

        public ObservableCollection<LiabilityExtendedModel> CivilLiabilities
        {
            get => civilliabilities;
            set => SetProperty(ref civilliabilities, value);
        }

        public ObservableCollection<LiabilityExtendedModel> CarInsurances
        {
            get => carInsurances;
            set => SetProperty(ref carInsurances, value);
        }

        public ObservableCollection<LiabilityExtendedModel> Vignettes
        {
            get => vignettes;
            set => SetProperty(ref vignettes, value);
        }

        public ObservableCollection<RepairModel> Repairs
        {
            get => repairs;
            set => SetProperty(ref repairs, value);
        }

        public RepairModel SelectedRepair
        {
            get => selectedRepair;
            set
            {
                SetProperty(ref selectedRepair, value);
                RaisePropertyChanged(() => HasSelectedRepair);

                if (selectedRepair == null)
                    return;

                SelectedRepairShop = RepairShops.FirstOrDefault(r => r.Id == selectedRepair.RepairShopId);
                RaisePropertyChanged(() => SelectedRepairShop);
                RaisePropertyChanged(() => RepairPrice);
                RaisePropertyChanged(() => BasePrice);
                RaisePropertyChanged(() => Vat);
                RaisePropertyChanged(() => CanSaveRepair);
            }
        }

        public ObservableCollection<RepairShopModel> RepairShops
        {
            get => repairShops;
            set => SetProperty(ref repairShops, value);
        }

        public RepairShopModel SelectedRepairShop
        {
            get => selectedRepairShop;
            set => SetProperty(ref selectedRepairShop, value);
        }

        public decimal RepairPrice
        {
            get => SelectedRepair?.FinalPrice ?? 0M;
            set
            {
                if (SelectedRepair == null)
                    return;

                SelectedRepair.FinalPrice = value;
                RaisePropertyChanged(() => RepairPrice);
                RaisePropertyChanged(() => BasePrice);
                RaisePropertyChanged(() => Vat);
                RaisePropertyChanged(() => CanSaveRepair);
            }
        }

        public decimal VatRate => vatRate;
        public string BasePrice => vatService.GetFormattedBasePrice(RepairPrice);
        public string Vat => vatService.GetFormattedVat(RepairPrice);

        public ObservableCollection<BasicEmployeeModel> EmployeesForVehicle
        {
            get => employeesForVehicle;
            set => SetProperty(ref employeesForVehicle, value);
        }

        public BasicEmployeeModel AssignedEmployee
        {
            get => assignedEmployee;
            set
            {
                SetProperty(ref assignedEmployee, value);
                RaisePropertyChanged(() => CanAssignEmployee);
            }
        }

        public ObservableCollection<BasicEmployeeModel> AllEmployees
        {
            get => allEmployees;
            set => SetProperty(ref allEmployees, value);
        }

        public BasicEmployeeModel NonAssignedEmployee
        {
            get => nonAssignedEmployee;
            set
            {
                SetProperty(ref nonAssignedEmployee, value);
                RaisePropertyChanged(() => CanAssignEmployee);
            }
        }

        public int SelectedTab
        {
            get => selectedTab;
            set
            {
                if (selectedTab == 0 && Vehicle != null)
                {
                    Vehicle.VehicleType = vehicleType.Id;
                    Vehicle.MakeId = Make.Id;
                    Vehicle.MakeName = Make.Name;
                    Vehicle.ModelId = Model.Id;
                    Vehicle.ModelName = Model.Name;
                    Vehicle.Fuel = FuelType.Id;
                    Vehicle.Color = Color;
                    Vehicle.LicencePlate = LicencePlate;
                    Vehicle.ImageAddress = ImageAddress;
                    Vehicle.IsBlocked = IsBlocked;
                }

                SetProperty(ref selectedTab, value);
                Task.Run(LoadBindings);
            }
        }

        public ObservableCollection<FuelTypeModel> FuelTypes => VehicleResources.FuelTypes.ToObservableCollection();
        public ObservableCollection<VehicleTypeModel> VehicleTypes => VehicleResources.VehicleTypes.ToObservableCollection();
        public ObservableCollection<string> Colors => VehicleResources.Colors.ToObservableCollection();

        public IMvxCommand SelectImageCommand { get; private set; }
        public IMvxCommand RemoveImageCommand { get; private set; }
        public IMvxCommand SaveCommand { get; private set; }
        public IMvxCommand CancelCommand { get; private set; }
        public IMvxCommand<LiabilityType> CreateLiabilityCommand { get; private set; }
        public IMvxCommand<LiabilityExtendedModel> EditLiabilityCommand { get; private set; }
        public IMvxCommand<LiabilityExtendedModel> DeleteLiabilityCommand { get; private set; }
        public IMvxCommand<string> RepairCheckedCommand { get; private set; }
        public IMvxCommand SaveRepairCommand { get; private set; }
        public IMvxCommand DeleteRepairCommand { get; private set; }
        public IMvxCommand AddVehicleForEmployeeCommand { get; private set; }
        public IMvxCommand RemoveVehicleForEmployeeCommand { get; private set; }
        public IMvxCommand<bool> BlockVehicleCommandCommand { get; private set; }
        public IMvxCommand RefreshCommand { get; private set; }

        public IMvxInteraction<UploadInteractionHandler> UploadInteraction => uploadInteraction;

        public EditVehicleViewModel(IApiService apiService, IMvxNavigationService navigationService, IVatService vatService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            this.vatService = vatService;

            SelectImageCommand = new MvxCommand(SelectImage);
            RemoveImageCommand = new MvxCommand(RemoveImage);
            SaveCommand = new MvxAsyncCommand(SaveVehicle);
            CancelCommand = new MvxCommand(GoHome);
            CreateLiabilityCommand = new MvxCommand<LiabilityType>((liabilityType)
                => navigationService.Navigate<AddLiabilityViewModel, NavigationModel<LiabilityExtendedModel>>(
                    new NavigationModel<LiabilityExtendedModel>
                    {
                        Data = new LiabilityExtendedModel { LiabilityType = liabilityType, LicencePlate = Vehicle.LicencePlate, VehicleId = Vehicle.Id },
                        Callback = GetVehicleExtended
                    }));
            EditLiabilityCommand = new MvxCommand<LiabilityExtendedModel>((liability)
                => navigationService.Navigate<EditLiabilityViewModel, NavigationModel<LiabilityExtendedModel>>(
                    new NavigationModel<LiabilityExtendedModel>
                    {
                        Data = liability,
                        Callback = GetLiabilities
                    }));

            DeleteLiabilityCommand = new MvxAsyncCommand<LiabilityExtendedModel>(DeleteLiability);
            RepairCheckedCommand = new MvxCommand<string>(RepairChecked);
            SaveRepairCommand = new MvxAsyncCommand(SaveRepair);
            DeleteRepairCommand = new MvxAsyncCommand(DeleteRepair);
            AddVehicleForEmployeeCommand = new MvxAsyncCommand(AddVehicleForEmployee);
            RemoveVehicleForEmployeeCommand = new MvxAsyncCommand(RemoveVehicleForEmployee);
            BlockVehicleCommandCommand = new MvxAsyncCommand<bool>(BlockVehicle);
            RefreshCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<EditVehicleViewModel>());
        }

        public override async Task Initialize()
        {
            await GetVehicles();
            await base.Initialize();
        }

        private async Task LoadBindings()
        {
            if (Vehicle == null)
                return;

            if (SelectedTab == 0)
            {
                VehicleType = VehicleTypes[Vehicle.VehicleType];
                Make = Makes.FirstOrDefault(m => m.Id == Vehicle.MakeId);
                FuelType = FuelTypes[Vehicle.Fuel];
                Color = Vehicle.Color;
                ImageAddress = Vehicle.ImageAddress;
                LicencePlate = Vehicle.LicencePlate;
                IsBlocked = Vehicle.IsBlocked;
                await GetVehicleExtended();
            }

            await GetLiabilities();
            await GetRepairShops();
            await GetRepairs();
            await GetAllEmployees();
        }

        private async Task GetLiabilities()
        {
            if (SelectedTab != 1)
                return;

            await GetMots();
            await GetCivilLiabilities();
            await GetCarInsurances();
            await GetVignettes();
        }

        private async Task GetMots()
        {
            var response = await ApiService.GetAsync<GetLiabilitiesDto>($"mots/vehicle/{Vehicle.Id}");

            if (response.IsSuccessStatusCode)
                MOTs = response.Content.Liabilities.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetCivilLiabilities()
        {
            var response = await ApiService.GetAsync<GetLiabilitiesDto>($"civilliabilities/vehicle/{Vehicle.Id}");

            if (response.IsSuccessStatusCode)
                CivilLiabilities = response.Content.Liabilities.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetCarInsurances()
        {
            var response = await ApiService.GetAsync<GetLiabilitiesDto>($"carinsurances/vehicle/{Vehicle.Id}");

            if (response.IsSuccessStatusCode)
                CarInsurances = response.Content.Liabilities.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetVignettes()
        {
            var response = await ApiService.GetAsync<GetLiabilitiesDto>($"vignettes/vehicle/{Vehicle.Id}");

            if (response.IsSuccessStatusCode)
                Vignettes = response.Content.Liabilities.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetVehicles()
        {
            var response = await ApiService.GetAsync<GetVehiclesDto>("vehicles");

            if (response.IsSuccessStatusCode)
            {
                Vehicles = response.Content.Vehicles.ToObservableCollection();
                Vehicle = Vehicles.FirstOrDefault();
                await GetMakes();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetVehicleExtended()
        {
            var response = await ApiService.GetAsync<VehicleExtendedModel>($"vehicles/{Vehicle.Id}/extended");

            if (response.IsSuccessStatusCode)
            {
                RecentLiabilities = response.Content.RecentLiabilities;
                EmployeesForVehicle = response.Content.Employees.ToObservableCollection();
                AssignedEmployee = EmployeesForVehicle.FirstOrDefault();
                await GetLiabilities();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetMakes()
        {
            var response = await ApiService.GetAsync<GetMakesDto>("makes");

            if (response.IsSuccessStatusCode)
            {
                Makes = response.Content.Makes.ToObservableCollection();
                Make = Makes.First(m => m.Id == Vehicle.MakeId);
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetModels()
        {
            if (Make == null || VehicleType == null)
                return;

            var response = await ApiService.GetAsync<GetModelsDto>($"models/make/{Make.Id}/type/{VehicleType.Id}");

            if (response.IsSuccessStatusCode)
                Models = response.Content.Models.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetRepairs()
        {
            if (SelectedTab != 2)
                return;

            var response = await ApiService.GetAsync<GetRepairsDto>($"repairs/vehicle/{Vehicle.Id}");
            if (response.IsSuccessStatusCode)
            {
                Repairs = response.Content.Repairs.ToObservableCollection();
                SelectedRepair = Repairs.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetRepairShops()
        {
            if (SelectedTab != 2)
                return;

            var response = await ApiService.GetAsync<GetRepairShopsDto>($"repairshops");
            if (response.IsSuccessStatusCode)
                RepairShops = response.Content.RepairShops.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetAllEmployees()
        {
            if (SelectedTab != 3)
                return;

            var response = await ApiService.GetAsync<GetEmployeesDto>("employees");

            if (response.IsSuccessStatusCode)
                AllEmployees = response.Content.Employees.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private void SelectImage()
        {
            uploadInteraction.Raise(
                new UploadInteractionHandler
                {
                    Callback = (filename) => AddImage(filename)
                });
        }

        private void AddImage(string filename)
        {
            ImageAddress = filename;
            isImageUpdated = true;
        }

        private void RemoveImage()
        {
            ImageAddress = string.Empty;
            isImageUpdated = true;
        }

        private async Task SaveVehicle()
        {
            if (!IsValid)
            {
                RaiseNotification("Моля въведете всички необходими данни", "Грешка!!!");
                return;
            }

            bool isImageStored = await StoreImage();

            Vehicle.MakeId = Make.Id;
            Vehicle.MakeName = Make.Name;
            Vehicle.ModelId = Model.Id;
            Vehicle.ModelName = Model.Name;
            Vehicle.Fuel = FuelType.Id;
            Vehicle.Color = Color;
            Vehicle.VehicleType = VehicleType.Id;
            Vehicle.LicencePlate = LicencePlate.ToUpper();

            var result = await ApiService.PutAsync<int>($"vehicles/{vehicle.Id}", vehicle);

            if (result.IsSuccessStatusCode)
            {
                await RaisePropertyChanged(() => Vehicles);
                await RaisePropertyChanged(() => Vehicle);
                await RaisePropertyChanged(() => IsValid);
                return;
            }

            if (isImageStored && !string.IsNullOrEmpty(Vehicle.ImageName))
                await ApiService.DeleteFileAsync(Vehicle.ImageName);

            RaiseNotification(result.Error, "Грешка!!!");
        }

        private async Task<bool> StoreImage()
        {
            if (!isImageUpdated)
                return false;

            if (!string.IsNullOrEmpty(Vehicle.ImageAddress))
                await ApiService.DeleteFileAsync(Vehicle.ImageName);

            if (string.IsNullOrEmpty(ImageAddress))
            {
                vehicle.ImageName = null;
                isImageUpdated = false;
                return true;
            }

            var response = await ApiService.UploadAsync(ImageAddress);
            if (response.IsSuccessStatusCode)
            {
                vehicle.ImageName = response.Content;
                isImageUpdated = false;
                return true;
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");

            return false;
        }

        private async Task DeleteLiability(LiabilityExtendedModel model)
        => await ShowMessage(
                $"Сигурни ли сте, че желаете да изтриете {LiabilityResources.GetTranslatedLiability(model.LiabilityType)} с начална дата {model.StartDate.ToShortDateString()} от базата?",
                $"Изтриване на {LiabilityResources.GetTranslatedLiability(model.LiabilityType)}",
                () => OnDeleteConfirm(model));

        private async Task OnDeleteConfirm(LiabilityExtendedModel model)
        {
            var result = await ApiService.DeleteAsync<string>($"{LiabilityResources.GetLiabilityController(model.LiabilityType)}/{model.Id}");

            if (result.IsSuccessStatusCode)
                switch (model.LiabilityType)
                {
                    case LiabilityType.MOT:
                        MOTs.Remove(model);
                        break;
                    case LiabilityType.CivilLiability:
                        CivilLiabilities.Remove(model);
                        break;
                    case LiabilityType.CarInsurance:
                        carInsurances.Remove(model);
                        break;
                    case LiabilityType.Vignette:
                        vignettes.Remove(model);
                        break;
                    default:
                        break;
                }
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }

        private void RepairChecked(string propertyName)
        {
            var property = SelectedRepair.GetType().GetProperty(propertyName);
            if (property == null)
                return;

            property.SetValue(SelectedRepair, !(bool)property.GetValue(SelectedRepair));
            RaisePropertyChanged(() => SelectedRepair);
        }

        private async Task SaveRepair()
        {
            if (!CanSaveRepair)
                return;

            SelectedRepair.RepairShopId = SelectedRepairShop.Id;
            SelectedRepair.RepairShopName = SelectedRepairShop.Name;
            SelectedRepair.FinalPrice = RepairPrice;
            int id = SelectedRepair.Id;

            var response = await ApiService.PutAsync<string>($"repairs?id={SelectedRepair.Id}", SelectedRepair);

            if (response.IsSuccessStatusCode)
            {
                await GetRepairs();
                SelectedRepair = Repairs.FirstOrDefault(r => r.Id == id);
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task DeleteRepair()
        => await ShowMessage(
                $"Сигурни ли сте, че желаете да изтриете ремонт от сервиз {SelectedRepair.RepairShopName} от {SelectedRepair.Date.ToShortDateString()} от базата?",
                $"Изтриване на ремонт",
                () => OnDeleteRepairConfirm());

        private async Task OnDeleteRepairConfirm()
        {
            var result = await ApiService.DeleteAsync<string>($"repairs/{SelectedRepair.Id}");

            if (result.IsSuccessStatusCode)
                await GetRepairs();
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }

        private async Task AddVehicleForEmployee() 
            => await NavigationService.Navigate<RoadBookEntryAddViewModel, RoadBookNavigationModel>(new RoadBookNavigationModel
            {
                Callback = CompleteAddVehicleForEmployee,
                Id = Vehicle.ActiveRecordEntryId,
            });

        private async Task CompleteAddVehicleForEmployee(RoadBookBasicEntryModel entry)
        {
            entry.EmployeeId = nonAssignedEmployee.Id;
            entry.VehicleId = Vehicle.Id;
            var response = await ApiService.PostAsync<int>($"employees/{nonAssignedEmployee.Id}/vehicle/{Vehicle.Id}", entry);

            if (response.IsSuccessStatusCode)
            {
                EmployeesForVehicle.Add(NonAssignedEmployee);
                await RaisePropertyChanged(() => CanAssignEmployee);
                Vehicle.ActiveRecordEntryId = response.Content;
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task RemoveVehicleForEmployee()
            => await NavigationService.Navigate<RoadBookEntryReturnViewModel, RoadBookNavigationModel>(new RoadBookNavigationModel
            {
                Callback = CompleteRemoveVehicleForEmployee,
                Id = Vehicle.ActiveRecordEntryId,
            });

        private async Task CompleteRemoveVehicleForEmployee(RoadBookBasicEntryModel entry)
        {
            entry.EmployeeId = AssignedEmployee.Id;
            var response = await ApiService.DeleteAsync<int>($"employees/{AssignedEmployee.Id}/vehicle/{Vehicle.Id}", entry);

            if (!response.IsSuccessStatusCode)
            {
                RaiseNotification(response.Error, "Грешка!!!");
                return;
            }
            
            EmployeesForVehicle.Remove(AssignedEmployee);
            await RaisePropertyChanged(() => CanAssignEmployee);
            Vehicle.ActiveRecordEntryId = response.Content;
        }

        private async Task BlockVehicle(bool isBlockedParam)
        {
            if (isBlockedParam == IsBlocked)
                return;

            var response = await ApiService.PostAsync<int>($"vehicles/{Vehicle.Id}/blocked", new { Id = Vehicle.Id, IsBlocked = isBlockedParam });

            if (!response.IsSuccessStatusCode)
            {
                RaiseNotification(response.Error, "Грешка!!!");
                return;
            }

            Vehicle.IsBlocked = isBlockedParam;
            IsBlocked = isBlockedParam;
        }

    }
}
