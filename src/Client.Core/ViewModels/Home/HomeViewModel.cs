using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Models.Issues;
using Client.Core.Models.Liabilities;
using Client.Core.Models.Vehicles;
using Client.Core.Properties;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Account;
using Client.Core.ViewModels.Common;
using Client.Core.ViewModels.Employees;
using Client.Core.ViewModels.Issues;
using Client.Core.ViewModels.Liabilities;
using Client.Core.ViewModels.Mileages;
using Client.Core.ViewModels.Repairs;
using Client.Core.ViewModels.RoadBook;
using Client.Core.ViewModels.Settings;
using Client.Core.ViewModels.Vehicles;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using AppSettings = Client.Core.Properties.Settings;

namespace Client.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IAuthService authService;
        private readonly ICurrentUserService currentUserService;
        private MvxInteraction closeAppInteraction = new MvxInteraction();

        private IssuesCountModel issues;
        private AllVehiclesSummaryModel vehicleSummary;

        public IMvxInteraction CloseAppInteraction => closeAppInteraction;

        public IssuesCountModel Issues
        {
            get => issues;
            set
            {
                issues = value;
                RaiseAllPropertiesChanged();
            }
        }

        public bool IsAdmin => currentUserService.CurrentUser.IsAdmin;

        public int BeltMileageWarnings => AppSettings.Default.EnableBeltNotifications && Issues != null ? Issues.BeltMileageWarnings : 0;
        public int BeltMileageAlerts => AppSettings.Default.EnableBeltNotifications && Issues != null ? Issues.BeltMileageAlerts : 0;
        public int BrakeLiningsMileageWarnings => AppSettings.Default.EnableBrakeLiningsNotifications && Issues != null ? Issues.BrakeLiningsMileageWarnings : 0;
        public int BrakeLiningsMileageAlerts => AppSettings.Default.EnableBrakeLiningsNotifications && Issues != null ? Issues.BrakeLiningsMileageAlerts : 0;
        public int BrakeDisksMileageWarnings => AppSettings.Default.EnableBrakeDisksNotifications && Issues != null ? Issues.BrakeDisksMileageWarnings : 0;
        public int BrakeDisksMileageAlerts => AppSettings.Default.EnableBrakeDisksNotifications && Issues != null ? Issues.BrakeDisksMileageAlerts : 0;
        public int CoolantMileageWarnings => AppSettings.Default.EnableCoolantNotifications && Issues != null ? Issues.CoolantMileageWarnings : 0;
        public int CoolantMileageAlerts => AppSettings.Default.EnableCoolantNotifications && Issues != null ? Issues.CoolantMileageAlerts : 0;
        public int OilMileageWarnings => AppSettings.Default.EnableOilNotifications && Issues != null ? Issues.OilMileageWarnings : 0;
        public int OilMileageAlerts => AppSettings.Default.EnableOilNotifications && Issues != null ? Issues.OilMileageAlerts : 0;
        public int MotWarnings => AppSettings.Default.EnableMotNotifications && Issues != null ? Issues.MotWarnings : 0;
        public int MotAlerts => AppSettings.Default.EnableMotNotifications && Issues != null ? Issues.MotAlerts : 0;
        public int CivilLiabilityWarnings => AppSettings.Default.EnableCivilLiabilityNotifications && Issues != null ? Issues.CivilLiabilityWarnings : 0;
        public int CivilLiabilityAlerts => AppSettings.Default.EnableCivilLiabilityNotifications && Issues != null ? Issues.CivilLiabilityAlerts : 0;
        public int CarInsuranceWarnings => AppSettings.Default.EnableCarInsuranceNotifications && Issues != null ? Issues.CarInsuranceWarnings : 0;
        public int CarInsuranceAlerts => AppSettings.Default.EnableCarInsuranceNotifications && Issues != null ? Issues.CarInsuranceAlerts : 0;
        public int VignetteWarnings => AppSettings.Default.EnableVignetteNotifications && Issues != null ? Issues.VignetteWarnings : 0;
        public int VignetteAlerts => AppSettings.Default.EnableVignetteNotifications && Issues != null ? Issues.VignetteAlerts : 0;
        public int Total => BeltMileageAlerts + BeltMileageWarnings +
            BrakeDisksMileageAlerts + BrakeDisksMileageWarnings +
            BrakeLiningsMileageAlerts + BrakeLiningsMileageWarnings +
            CoolantMileageAlerts + CoolantMileageWarnings +
            OilMileageAlerts + OilMileageWarnings +
            MotAlerts + MotWarnings +
            CarInsuranceAlerts + CarInsuranceWarnings +
            CivilLiabilityAlerts + CivilLiabilityWarnings +
            VignetteAlerts + VignetteWarnings;

        public AllVehiclesSummaryModel VehicleSummary
        {
            get => vehicleSummary;
            set => SetProperty(ref vehicleSummary, value);
        }

        public IMvxCommand GoHomeCommand { get; private set; }
        public IMvxCommand GoToAddVehicleCommand { get; private set; }
        public IMvxCommand GoToEditVehicleCommand { get; private set; }
        public IMvxCommand GoToDeleteVehicleCommand { get; private set; }
        public IMvxCommand GoToAddRepairCommand { get; private set; }
        public IMvxCommand GoToMileageReportCommand { get; private set; }
        public IMvxCommand GoToEmployeesCommand { get; private set; }
        public IMvxCommand GoToRoadBookCommand { get; private set; }
        public IMvxCommand GoToRepairsCommand { get; private set; }
        public IMvxCommand GoToMotsCommand { get; private set; }
        public IMvxCommand GoToCivilLiabilitiesCommand { get; private set; }
        public IMvxCommand GoToCarInsurancesCommand { get; private set; }
        public IMvxCommand GoToVignettesCommand { get; private set; }
        public IMvxCommand GoToBeltIssuesCommand { get; private set; }
        public IMvxCommand GoToOilIssuesCommand { get; private set; }
        public IMvxCommand GoToBrakeLiningIssuesCommand { get; private set; }
        public IMvxCommand GoToBrakeDisksIssuesCommand { get; private set; }
        public IMvxCommand GoToCoolantIssuesCommand { get; private set; }
        public IMvxCommand GoToVehiclesCommand { get; private set; }
        public IMvxCommand CloseAppCommand { get; private set; }
        public IMvxCommand GoToSettingsCommand { get; private set; }
        public IMvxCommand GoToUsersCommand { get; private set; }
        public IMvxCommand GoToUserProfileCommand { get; private set; }
        public IMvxCommand GoToChangePasswordCommand { get; private set; }
        public IMvxCommand LogoutCommand { get; private set; }

        public HomeViewModel(IAuthService authService, ICurrentUserService currentUserService, IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            this.authService = authService;
            this.currentUserService = currentUserService;

            GoHomeCommand = new MvxCommand(() => navigationService.Navigate<HomeViewModel>());
            GoToAddVehicleCommand = new MvxCommand(() => navigationService.Navigate<AddVehicleViewModel>());
            GoToEditVehicleCommand = new MvxCommand(() => navigationService.Navigate<EditVehicleViewModel>());
            GoToDeleteVehicleCommand = new MvxCommand(() => navigationService.Navigate<DeleteVehicleViewModel>());
            GoToAddRepairCommand = new MvxCommand(() => navigationService.Navigate<AddRepairViewModel>());
            GoToMileageReportCommand = new MvxAsyncCommand(() => navigationService.Navigate<MileagesViewModel>());
            GoToEmployeesCommand = new MvxCommand(() => navigationService.Navigate<EmployeesViewModel>());
            GoToRoadBookCommand = new MvxCommand(() => navigationService.Navigate<RoadBookViewModel>());
            GoToRepairsCommand = new MvxCommand(() => navigationService.Navigate<RepairsViewModel>());
            GoToMotsCommand = new MvxCommand(() => navigationService.Navigate<LiabilitiesViewModel, LiabilityType>(LiabilityType.MOT));
            GoToCivilLiabilitiesCommand = new MvxCommand(() => navigationService.Navigate<LiabilitiesViewModel, LiabilityType>(LiabilityType.CivilLiability));
            GoToCarInsurancesCommand = new MvxCommand(() => navigationService.Navigate<LiabilitiesViewModel, LiabilityType>(LiabilityType.CarInsurance));
            GoToVignettesCommand = new MvxCommand(() => navigationService.Navigate<LiabilitiesViewModel, LiabilityType>(LiabilityType.Vignette));
            GoToBeltIssuesCommand = new MvxCommand(() => navigationService.Navigate<IssuesViewModel, IssueType>(IssueType.BeltMileage));
            GoToOilIssuesCommand = new MvxCommand(() => navigationService.Navigate<IssuesViewModel, IssueType>(IssueType.OilMileage));
            GoToBrakeLiningIssuesCommand = new MvxCommand(() => navigationService.Navigate<IssuesViewModel, IssueType>(IssueType.BrakeLiningsMileage));
            GoToBrakeDisksIssuesCommand = new MvxCommand(() => navigationService.Navigate<IssuesViewModel, IssueType>(IssueType.BrakeDisksMileage));
            GoToCoolantIssuesCommand = new MvxCommand(() => navigationService.Navigate<IssuesViewModel, IssueType>(IssueType.CoolantMileage));
            GoToVehiclesCommand = new MvxCommand(() => navigationService.Navigate<VehiclesViewModel>());
            CloseAppCommand = new MvxCommand(() => closeAppInteraction.Raise());
            GoToSettingsCommand = new MvxCommand(() => navigationService.Navigate<SettingsViewModel>());
            GoToUsersCommand = new MvxCommand(() => navigationService.Navigate<UsersViewModel>());
            GoToUserProfileCommand = new MvxCommand(() => navigationService.Navigate<ProfileViewModel>());
            GoToChangePasswordCommand = new MvxCommand(() => navigationService.Navigate<PasswordChangeViewModel>());
            LogoutCommand = new MvxAsyncCommand(Logout);
        }

        public override async Task Initialize()
        {
            await GetIssues();
            await GetVehicleSummary();
        }

        private async Task GetIssues()
        {
            var parameters = new Dictionary<string, int>();
            parameters["BeltW"] = AppSettings.Default.BeltMileageWarningLimit;
            parameters["BeltA"] = AppSettings.Default.BeltMileageAlertLimit;
            parameters["BeltC"] = AppSettings.Default.BeltChangeMileage;
            parameters["BrakeLiningsW"] = AppSettings.Default.BrakeLiningsMileageWarningLimit;
            parameters["BrakeLiningsA"] = AppSettings.Default.BrakeLiningsMileageAlertLimit;
            parameters["BrakeLiningsC"] = AppSettings.Default.BrakeLiningsChangeMileage;
            parameters["BrakeDisksW"] = AppSettings.Default.BrakeDisksMileageWarningLimit;
            parameters["BrakeDisksA"] = AppSettings.Default.BrakeDisksMileageAlertLimit;
            parameters["BrakeDisksC"] = AppSettings.Default.BrakeDisksChangeMileage;
            parameters["CoolantW"] = AppSettings.Default.CoolantMileageWarningLimit;
            parameters["CoolantA"] = AppSettings.Default.CoolantMileageAlertLimit;
            parameters["CoolantC"] = AppSettings.Default.CoolantChangeMileage;
            parameters["OilW"] = AppSettings.Default.OilMileageWarningLimit;
            parameters["OilA"] = AppSettings.Default.OilMileageAlertLimit;
            parameters["OilC"] = AppSettings.Default.OilChangeMileage;
            parameters["MotW"] = AppSettings.Default.MotWarningLimit;
            parameters["MotA"] = AppSettings.Default.MotAlertLimit;
            parameters["CivilLiabilityW"] = AppSettings.Default.CivilLiabilityWarningLimit;
            parameters["CivilLiabilityA"] = AppSettings.Default.CivilLiabilityAlertLimit;
            parameters["CarInsuranceW"] = AppSettings.Default.CarInsuranceWarningLimit;
            parameters["CarInsuranceA"] = AppSettings.Default.CarInsuranceAlertLimit;
            parameters["VignetteW"] = AppSettings.Default.VignetteWarningLimit;
            parameters["VignetteA"] = AppSettings.Default.VignetteAlertLimit;

            var queryParams = string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}").ToList());
            var response = await ApiService.GetAsync<IssuesCountModel>($"issues?{queryParams}");

            if (response.IsSuccessStatusCode)
                Issues = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetVehicleSummary()
        {
            var response = await ApiService.GetAsync<AllVehiclesSummaryModel>($"vehicles/summary");

            if (response.IsSuccessStatusCode)
                VehicleSummary = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task Logout()
        {
            authService.Logout();
            await NavigationService.Navigate<LoginViewModel>();
        }
    }
}
