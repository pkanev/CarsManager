using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models;
using Client.Core.Models.Employees;
using Client.Core.Models.RoadBook;
using Client.Core.Models.Vehicles;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.Utils;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.RoadBook;
using Client.Core.ViewModels.Towns;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels.Employees
{
    public partial class EmployeesViewModel : SubPageViewModel
    {
        private const string NAME_PATTERN = @"^[a-zа-я\s\-]+$";
        private const string PHONE_PATTERN = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";

        private int selectedTab;
        private CreateEmployeeModel newEmployee = new CreateEmployeeModel();
        private ObservableCollection<TownModel> towns;
        private TownModel selectedTown;
        private string imageFile;
        private ObservableCollection<VehicleDto> availableVehiclesForNewEmployee;
        private ObservableCollection<VehicleDto> vehiclesForNewEmployee = new ObservableCollection<VehicleDto>();
        private VehicleDto selectedVehicleForNewEmployee = new VehicleDto();
        private VehicleDto nullVehicle = new VehicleDto();
        private MvxInteraction<UploadInteractionHandler> uploadInteraction = new MvxInteraction<UploadInteractionHandler>();

        private ObservableCollection<BasicEmployeeModel> employees;
        private BasicEmployeeModel selectedEmployee;
        private GetEmployeeDto employeeInfo;
        private ObservableCollection<TownModel> selectedEmployeeTowns;
        private TownModel selectedEmployeeTown;
        private string selectedEmployeeImageFile;
        private bool isImageUpdatedForSelectedEmployee;
        private ObservableCollection<BasicVehicleModel> selectedEmployeeVehicles;
        private ObservableCollection<VehicleDto> availableVehiclesForSelectedEmployee = new ObservableCollection<VehicleDto>();
        private VehicleDto selectedAvailableVehicleForSelectedEmployee;

        public int SelectedTab
        {
            get => selectedTab;
            set
            {
                SetProperty(ref selectedTab, value);
                if (selectedTab == 1)
                    Task.Run(GetEmployees);
            }
        }

        public CreateEmployeeModel NewEmployee
        {
            get => newEmployee;
            set
            {
                SetProperty(ref newEmployee, value);
                RaisePropertyChanged(() => NewGivenName);
                RaisePropertyChanged(() => NewSurname);
                RaisePropertyChanged(() => Telephone);
                RaisePropertyChanged(() => CanSaveNewEmployee);
            }
        }

        public string NewGivenName
        {
            get => newEmployee.GivenName;
            set
            {
                NewEmployee.GivenName = value;
                RaisePropertyChanged(() => NewGivenName);
                RaisePropertyChanged(() => NewSurname);
                RaisePropertyChanged(() => Telephone);
                RaisePropertyChanged(() => CanSaveNewEmployee);
            }
        }

        public string NewSurname
        {
            get => newEmployee.Surname;
            set
            {
                NewEmployee.Surname = value;
                RaisePropertyChanged(() => NewSurname);
                RaisePropertyChanged(() => CanSaveNewEmployee);
            }
        }

        public string Telephone
        {
            get => newEmployee.Telephone;
            set
            {
                NewEmployee.Telephone = value;
                RaisePropertyChanged(() => Telephone);
                RaisePropertyChanged(() => CanSaveNewEmployee);
            }
        }

        public string ImageFile
        {
            get => imageFile;
            set => SetProperty(ref imageFile, value);
        }

        public ObservableCollection<TownModel> Towns
        {
            get => towns;
            set => SetProperty(ref towns, value);
        }

        public TownModel SelectedTown
        {
            get => selectedTown;
            set
            {
                if (value == null)
                    return;

                SetProperty(ref selectedTown, value);
                NewEmployee.TownId = selectedTown.Id;
                NewEmployee.Town = selectedTown.Name;
            }
        }

        public ObservableCollection<VehicleDto> AvailableVehiclesForNewEmployee
        {
            get => availableVehiclesForNewEmployee;
            set
            {
                availableVehiclesForNewEmployee = value;
                availableVehiclesForNewEmployee.Insert(0, nullVehicle);
                RaisePropertyChanged(() => AvailableVehiclesForNewEmployee);
            }
        }

        public VehicleDto SelectedVehicleForNewEmployee
        {
            get => selectedVehicleForNewEmployee;
            set
            {
                SetProperty(ref selectedVehicleForNewEmployee, value);
                RaisePropertyChanged(() => SelectedVehicleForNewEmployeeColor);
                RaisePropertyChanged(() => CanAddVehicleToNewEmployee);
            }
        }

        public string SelectedVehicleForNewEmployeeColor
        {
            get => SelectedVehicleForNewEmployee?.Color ?? string.Empty;
        }

        public ObservableCollection<VehicleDto> VehiclesForNewEmployee
        {
            get => vehiclesForNewEmployee;
            set => SetProperty(ref vehiclesForNewEmployee, value);
        }

        public bool CanAddVehicleToNewEmployee => SelectedVehicleForNewEmployee != null
            && SelectedVehicleForNewEmployee != nullVehicle
            && !SelectedVehicleForNewEmployee.IsBlocked
            && !VehiclesForNewEmployee.Contains(SelectedVehicleForNewEmployee);

        public bool CanSaveNewEmployee => !string.IsNullOrWhiteSpace(NewGivenName)
            && Regex.IsMatch(NewGivenName.ToLower(), NAME_PATTERN)
            && !string.IsNullOrWhiteSpace(NewSurname)
            && Regex.IsMatch(NewSurname.ToLower(), NAME_PATTERN)
            && (string.IsNullOrWhiteSpace(NewEmployee.Telephone) || Regex.IsMatch(NewEmployee.Telephone, PHONE_PATTERN));

        public ObservableCollection<BasicEmployeeModel> Employees
        {
            get => employees;
            set => SetProperty(ref employees, value);
        }

        public BasicEmployeeModel SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                RaisePropertyChanged(() => SelectedEmployee);
                RaisePropertyChanged(() => CanDeleteEmployee);
                RaisePropertyChanged(() => CanReleaseEmployee);
                Task.Run(GetEmployeeInfo);
            }
        }

        public GetEmployeeDto EmployeeInfo
        {
            get => employeeInfo;
            set
            {
                SetProperty(ref employeeInfo, value);
                RaisePropertyChanged(() => SelectedEmployeeGivenName);
                RaisePropertyChanged(() => SelectedEmployeeSurname);
                RaisePropertyChanged(() => SelectedEmployeeTelephone);
                RaisePropertyChanged(() => IsValidSelectedEmployee);
                RaisePropertyChanged(() => CanAddSelectedAvailableVehicleForSelectedEmployee);
            }
        }

        public string SelectedEmployeeGivenName
        {
            get => EmployeeInfo?.Employee.GivenName;
            set
            {
                if (EmployeeInfo == null)
                    return;
                EmployeeInfo.Employee.GivenName = value;
                RaisePropertyChanged(() => SelectedEmployeeGivenName);
                RaisePropertyChanged(() => IsValidSelectedEmployee);
            }
        }

        public string SelectedEmployeeSurname
        {
            get => EmployeeInfo?.Employee.Surname;
            set
            {
                if (EmployeeInfo == null)
                    return;
                EmployeeInfo.Employee.Surname = value;
                RaisePropertyChanged(() => SelectedEmployeeSurname);
                RaisePropertyChanged(() => IsValidSelectedEmployee);
            }
        }

        public string SelectedEmployeeTelephone
        {
            get => EmployeeInfo?.Employee.Telephone;
            set
            {
                if (EmployeeInfo == null)
                    return;
                EmployeeInfo.Employee.Telephone = value;
                RaisePropertyChanged(() => SelectedEmployeeTelephone);
                RaisePropertyChanged(() => IsValidSelectedEmployee);
            }
        }

        public ObservableCollection<TownModel> SelectedEmployeeTowns
        {
            get => selectedEmployeeTowns;
            set => SetProperty(ref selectedEmployeeTowns, value);
        }

        public TownModel SelectedEmployeeTown
        {
            get => selectedEmployeeTown;
            set => SetProperty(ref selectedEmployeeTown, value);
        }

        public string SelectedEmployeeImageAddress
        {
            get => selectedEmployeeImageFile;
            set => SetProperty(ref selectedEmployeeImageFile, value);
        }

        public ObservableCollection<BasicVehicleModel> SelectedEmployeeVehicles
        {
            get => selectedEmployeeVehicles;
            set
            {
                SetProperty(ref selectedEmployeeVehicles, value);
                RaisePropertyChanged(() => CanAddSelectedAvailableVehicleForSelectedEmployee);
                RaisePropertyChanged(() => CanReleaseEmployee);
            }
        }

        public ObservableCollection<VehicleDto> AvailableVehiclesForSelectedEmployee
        {
            get => availableVehiclesForSelectedEmployee;
            set => SetProperty(ref availableVehiclesForSelectedEmployee, value);
        }

        public VehicleDto SelectedAvailableVehicleForSelectedEmployee
        {
            get => selectedAvailableVehicleForSelectedEmployee;
            set
            {
                SetProperty(ref selectedAvailableVehicleForSelectedEmployee, value);
                RaisePropertyChanged(() => CanAddSelectedAvailableVehicleForSelectedEmployee);
            }
        }

        public bool CanAddSelectedAvailableVehicleForSelectedEmployee =>
            SelectedEmployee != null
            && SelectedEmployee.IsEmployed
            && SelectedAvailableVehicleForSelectedEmployee != null
            && !SelectedAvailableVehicleForSelectedEmployee.IsBlocked
            && SelectedEmployeeVehicles != null
            && !SelectedEmployeeVehicles.Any(v => v.Id == SelectedAvailableVehicleForSelectedEmployee.Id);

        public bool CanDeleteEmployee => SelectedEmployee != null && IsAdmin;
        public bool CanReleaseEmployee => SelectedEmployee != null && SelectedEmployeeVehicles != null && SelectedEmployeeVehicles.Count == 0;

        public bool IsValidSelectedEmployee => EmployeeInfo?.Employee != null
            && !string.IsNullOrWhiteSpace(EmployeeInfo.Employee.GivenName)
            && Regex.IsMatch(EmployeeInfo.Employee.GivenName.ToLower(), NAME_PATTERN)
            && !string.IsNullOrWhiteSpace(EmployeeInfo.Employee.Surname)
            && Regex.IsMatch(EmployeeInfo.Employee.Surname.ToLower(), NAME_PATTERN)
            && (string.IsNullOrWhiteSpace(EmployeeInfo.Employee.Telephone) || Regex.IsMatch(EmployeeInfo.Employee.Telephone, PHONE_PATTERN));

        public IMvxInteraction<UploadInteractionHandler> UploadInteraction => uploadInteraction;

        public IMvxCommand<bool> SelectImageCommand { get; private set; }
        public IMvxCommand<bool> RemoveImageCommand { get; private set; }
        public IMvxCommand AddVehicleForNewEmployeeCommand { get; private set; }
        public IMvxCommand<VehicleDto> RemoveVehicleForNewEmployeeCommand { get; private set; }
        public IMvxCommand SaveNewEmployeeCommand { get; private set; }
        public IMvxCommand ClearNewEmployeeCommand { get; private set; }
        public IMvxCommand SaveSelectedEmployeeCommand { get; private set; }
        public IMvxCommand DeleteSelectedEmployeeCommand { get; private set; }
        public IMvxCommand AddVehicleForSelectedEmployeeCommand { get; private set; }
        public IMvxCommand<BasicVehicleModel> RemoveVehicleForSelectedEmployeeCommand { get; private set; }
        public IMvxCommand AddTownCommand { get; private set; }
        public IMvxCommand EditTownCommand { get; private set; }
        public IMvxCommand DeleteTownCommand { get; private set; }
        public IMvxCommand UpdateIsEmployedStatusCommand { get; private set; }

        public EmployeesViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            SelectImageCommand = new MvxCommand<bool>(SelectImage);
            RemoveImageCommand = new MvxCommand<bool>(RemoveImage);
            AddVehicleForNewEmployeeCommand = new MvxAsyncCommand(AddVehicleForNewEmployee);
            RemoveVehicleForNewEmployeeCommand = new MvxCommand<VehicleDto>(RemoveVehicleForNewEmployee);
            SaveNewEmployeeCommand = new MvxAsyncCommand(SaveNewEmployee);
            GoHomeCommand = new MvxCommand(GoHome);
            ClearNewEmployeeCommand = new MvxAsyncCommand(ClearForm);
            SaveSelectedEmployeeCommand = new MvxAsyncCommand(SaveSelectedEmployee);
            DeleteSelectedEmployeeCommand = new MvxAsyncCommand(DeleteSelectedEmployee);
            AddVehicleForSelectedEmployeeCommand = new MvxAsyncCommand(AddVehicleForSelectedEmployee);
            RemoveVehicleForSelectedEmployeeCommand = new MvxAsyncCommand<BasicVehicleModel>(RemoveVehicleForSelectedEmployee);
            AddTownCommand = new MvxCommand(() => NavigationService.Navigate<TownViewModel, NavigationModel<TownModel>>(new NavigationModel<TownModel> { Data = new TownModel(), Callback = GetTowns }));
            EditTownCommand = new MvxCommand(() => NavigationService.Navigate<TownViewModel, NavigationModel<TownModel>>(
                new NavigationModel<TownModel>
                {
                    Data = SelectedTab == 0 ? SelectedTown : SelectedEmployeeTown,
                    Callback = GetTowns
                }));
            DeleteTownCommand = new MvxAsyncCommand(DeleteTown);
            UpdateIsEmployedStatusCommand = new MvxAsyncCommand(UpdateIsEmployedStatus);
        }

        public override async Task Initialize()
        {
            await GetTowns();
            await GetVehicles();
            await base.Initialize();
        }

        private async Task GetTowns()
        {
            var response = await ApiService.GetAsync<GetTownsDto>("Towns");

            if (response.IsSuccessStatusCode)
            {
                Towns = response.Content.Towns.ToObservableCollection();
                SelectedTown = Towns.FirstOrDefault();
                SelectedEmployeeTowns = response.Content.Towns.ToObservableCollection();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetVehicles()
        {
            var response = await ApiService.GetAsync<GetVehiclesDto>("vehicles");

            if (response.IsSuccessStatusCode)
            {
                AvailableVehiclesForNewEmployee = response.Content.Vehicles.ToObservableCollection();
                AvailableVehiclesForSelectedEmployee = response.Content.Vehicles.ToObservableCollection();
                SelectedVehicleForNewEmployee = AvailableVehiclesForNewEmployee.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetEmployees()
        {
            var response = await ApiService.GetAsync<GetEmployeesDto>("employees");

            if (response.IsSuccessStatusCode)
            {
                Employees = response.Content.Employees.ToObservableCollection();
                SelectedEmployee = Employees.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetEmployeeInfo()
        {
            if (SelectedEmployee == null)
                return;

            var response = await ApiService.GetAsync<GetEmployeeDto>($"employees/{SelectedEmployee.Id}");

            if (response.IsSuccessStatusCode)
            {
                EmployeeInfo = response.Content;
                SelectedEmployeeTown = SelectedEmployeeTowns.FirstOrDefault(t => t.Id == EmployeeInfo.Employee.TownId);
                SelectedEmployeeImageAddress = EmployeeInfo.Employee.ImageAddress;
                SelectedEmployeeVehicles = EmployeeInfo.Vehicles.ToObservableCollection();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private void SelectImage(bool isNew) => uploadInteraction
            .Raise(new UploadInteractionHandler
            {
                Callback = (filename) =>
                {
                    if (isNew)
                        ImageFile = filename;
                    else
                    {
                        SelectedEmployeeImageAddress = filename;
                        isImageUpdatedForSelectedEmployee = true;
                    }
                }
            });

        private void RemoveImage(bool isNew)
        {
            if (isNew)
                ImageFile = null;
            else
            {
                SelectedEmployeeImageAddress = null;
                isImageUpdatedForSelectedEmployee = true;
            }
        }

        private async Task AddVehicleForNewEmployee()
        {
            if (VehiclesForNewEmployee.Contains(SelectedVehicleForNewEmployee) || SelectedVehicleForNewEmployee == nullVehicle)
                return;

            await NavigationService.Navigate<RoadBookEntryAddViewModel, RoadBookNavigationModel>(new RoadBookNavigationModel
            {
                Callback = CompleteAddVehicleForNewEmployee,
                Id = SelectedVehicleForNewEmployee.ActiveRecordEntryId,
            });
        }

        private Task CompleteAddVehicleForNewEmployee(RoadBookBasicEntryModel entry)
        {
            VehiclesForNewEmployee.Add(SelectedVehicleForNewEmployee);
            entry.VehicleId = SelectedVehicleForNewEmployee.Id;
            NewEmployee.RoadBookEntries.Add(entry);
            AvailableVehiclesForNewEmployee.Remove(SelectedVehicleForNewEmployee);
            SelectedVehicleForNewEmployee = nullVehicle;
            return Task.CompletedTask;
        }

        private void RemoveVehicleForNewEmployee(VehicleDto vehicle)
        {
            VehiclesForNewEmployee.Remove(vehicle);
            AvailableVehiclesForNewEmployee.Add(vehicle);
            var roadBookEntry = NewEmployee.RoadBookEntries.FirstOrDefault(e => e.VehicleId == vehicle.Id);
            if (roadBookEntry != null)
                NewEmployee.RoadBookEntries.Remove(roadBookEntry);
        }

        private async Task SaveNewEmployee()
        {
            if (!CanSaveNewEmployee)
            {
                RaiseNotification("Моля въведете всички необходими данни", "Грешка!!!");
                return;
            }

            await StoreImageForNewEmployee();

            foreach (var vehicle in VehiclesForNewEmployee)
                NewEmployee.VehicleIds.Add(vehicle.Id);

            var result = await ApiService.PostAsync<int>("employees", NewEmployee);

            if (result.IsSuccessStatusCode)
            {
                var employeeId = result.Content;
                await ClearForm();
                return;
            }

            if (!string.IsNullOrEmpty(NewEmployee.ImageName))
                await ApiService.DeleteFileAsync(NewEmployee.ImageName);

            RaiseNotification(result.Error, "Грешка!!!");
        }

        private async Task StoreImageForNewEmployee()
        {
            if (string.IsNullOrEmpty(ImageFile))
                return;

            var result = await ApiService.UploadAsync(imageFile);
            if (result.IsSuccessStatusCode)
                NewEmployee.ImageName = result.Content;
        }

        private async Task ClearForm()
        {
            NewEmployee = new CreateEmployeeModel();
            ImageFile = null;
            VehiclesForNewEmployee = new ObservableCollection<VehicleDto>();
            SelectedTown = Towns.FirstOrDefault();
            SelectedVehicleForNewEmployee = nullVehicle;
            await GetVehicles();
        }

        private async Task SaveSelectedEmployee()
        {
            if (SelectedEmployee == null || EmployeeInfo == null)
                return;

            bool isImageStored = await StoreImageForSelectedEmployee();

            EmployeeInfo.Employee.TownId = selectedEmployeeTown.Id;

            var response = await ApiService.PutAsync<string>($"employees/{SelectedEmployee.Id}", EmployeeInfo.Employee);

            if (response.IsSuccessStatusCode)
                return;

            if (isImageStored)
                await ApiService.DeleteFileAsync(EmployeeInfo.Employee.ImageName);

            RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task DeleteSelectedEmployee()
        {
            if (SelectedEmployee == null)
                return;

            await ShowMessage($"Сигурни ли сте, че желаете да изтриете {SelectedEmployee.GivenName} {SelectedEmployee.Surname} от базата?", "Изтриване на служител", async() =>
            {
                var response = await ApiService.DeleteAsync<string>($"employees/{SelectedEmployee.Id}");

                if (response.IsSuccessStatusCode)
                {
                    SelectedEmployeeVehicles.Clear();
                    Employees.Remove(SelectedEmployee);
                }
                else
                    RaiseNotification(response.Error, "Грешка!!!");
            });
        }

        private async Task AddVehicleForSelectedEmployee()
        {
            if (SelectedEmployee == null
                || SelectedAvailableVehicleForSelectedEmployee == null
                || !CanAddSelectedAvailableVehicleForSelectedEmployee)
                return;

            await NavigationService.Navigate<RoadBookEntryAddViewModel, RoadBookNavigationModel>(new RoadBookNavigationModel
               {
                   Callback = CompleteAddVehicleForEmployee,
                   Id = SelectedAvailableVehicleForSelectedEmployee.ActiveRecordEntryId,
               });
        }

        private async Task CompleteAddVehicleForEmployee(RoadBookBasicEntryModel entry)
        {
            entry.EmployeeId = SelectedEmployee.Id;
            entry.VehicleId = SelectedAvailableVehicleForSelectedEmployee.Id;
            var response = await ApiService.PostAsync<int>($"employees/{SelectedEmployee.Id}/vehicle/{SelectedAvailableVehicleForSelectedEmployee.Id}", entry);

            if (!response.IsSuccessStatusCode)
            {
                RaiseNotification(response.Error, "Грешка!!!");
                return;
            }

            SelectedEmployeeVehicles.Add(new BasicVehicleModel
            {
                Id = SelectedAvailableVehicleForSelectedEmployee.Id,
                LicencePlate = SelectedAvailableVehicleForSelectedEmployee.LicencePlate,
                Color = SelectedAvailableVehicleForSelectedEmployee.Color,
                Make = SelectedAvailableVehicleForSelectedEmployee.MakeName,
                Model = SelectedAvailableVehicleForSelectedEmployee.ModelName,
                Mileage = SelectedAvailableVehicleForSelectedEmployee.Mileage,
                ActiveRecordEntryId = response.Content
            });

            SelectedAvailableVehicleForSelectedEmployee.ActiveRecordEntryId = response.Content;
            await RaisePropertyChanged(() => CanAddSelectedAvailableVehicleForSelectedEmployee);
            await RaisePropertyChanged(() => CanReleaseEmployee);
        }

        private async Task RemoveVehicleForSelectedEmployee(BasicVehicleModel vehicle)
        {
            if (SelectedEmployee == null || vehicle == null)
                return;

            await NavigationService.Navigate<RoadBookEntryReturnViewModel, RoadBookNavigationModel>(new RoadBookNavigationModel
            {
                Callback = CompleteRemoveVehicleForEmployee,
                Id = vehicle.ActiveRecordEntryId,
            });
        }

        private async Task CompleteRemoveVehicleForEmployee(RoadBookBasicEntryModel entry)
        {
            var vehicle = SelectedEmployeeVehicles.FirstOrDefault(v => v.Id == entry.VehicleId);
            if (vehicle == null)
            {
                RaiseNotification("Има грешка. Опитайте да презаредите страницата.", "Грешка!!!");
                return;
            }

            entry.EmployeeId = SelectedEmployee.Id;
            var response = await ApiService.DeleteAsync<int>($"employees/{SelectedEmployee.Id}/vehicle/{entry.VehicleId}", entry);

            if (!response.IsSuccessStatusCode)
            {
                RaiseNotification(response.Error, "Грешка!!!");
                return;
            }

            vehicle.ActiveRecordEntryId = response.Content;
            SelectedEmployeeVehicles.Remove(vehicle);
            var vehicleInList = AvailableVehiclesForSelectedEmployee.FirstOrDefault(v => v.Id == vehicle.Id);
            if (vehicleInList == null)
            {
                RaiseNotification("Има грешка. Опитайте да презаредите страницата.", "Грешка!!!");
                return;
            }

            var availableVehicle = AvailableVehiclesForSelectedEmployee.FirstOrDefault(v => v.Id == vehicle.Id);
            if (availableVehicle != null)
                availableVehicle.Mileage = entry.NewMileage.GetValueOrDefault();

            vehicleInList.ActiveRecordEntryId = response.Content;
            await RaisePropertyChanged(() => CanAddSelectedAvailableVehicleForSelectedEmployee);
            await RaisePropertyChanged(() => SelectedAvailableVehicleForSelectedEmployee);
            await RaisePropertyChanged(() => CanReleaseEmployee);
        }

        private async Task<bool> StoreImageForSelectedEmployee()
        {
            if (EmployeeInfo?.Employee == null || !isImageUpdatedForSelectedEmployee)
                return false;

            if (!string.IsNullOrEmpty(EmployeeInfo.Employee.ImageAddress))
                await ApiService.DeleteFileAsync(EmployeeInfo.Employee.ImageName);

            if (string.IsNullOrEmpty(SelectedEmployeeImageAddress))
            {
                EmployeeInfo.Employee.ImageName = null;
                isImageUpdatedForSelectedEmployee = false;
                return true;
            }

            var response = await ApiService.UploadAsync(SelectedEmployeeImageAddress);
            if (response.IsSuccessStatusCode)
            {
                EmployeeInfo.Employee.ImageName = response.Content;
                isImageUpdatedForSelectedEmployee = false;
                return true;
            }

            RaiseNotification(response.Error, "Грешка!!!");
            return false;
        }

        private async Task DeleteTown()
        {
            var town = SelectedTab == 0
                ? SelectedTown
                : SelectedEmployeeTown;

            if (town == null)
                return;

            await ShowMessage($"Сигурни ли сте, че желаете да изтриете град \"{town.Name}\" от базата? Само градове, където няма регистрирани служители могат да бъдат изтрити.", "Изтриване на град", async () =>
                {
                    var response = await ApiService.DeleteAsync<string>($"towns/{town.Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        await GetTowns();
                    }
                    else
                        RaiseNotification(response.Error, "Грешка!!!");
                });
        }

        private async Task UpdateIsEmployedStatus()
        {
            var response = await ApiService.PatchAsync<string>($"employees/{SelectedEmployee.Id}", new { Id = SelectedEmployee.Id, IsEmployed = SelectedEmployee.IsEmployed });

            if (response.IsSuccessStatusCode)
            {
                await RaisePropertyChanged(() => CanAddSelectedAvailableVehicleForSelectedEmployee);
                return;
            }

            SelectedEmployee.IsEmployed = !SelectedEmployee.IsEmployed;
            RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
