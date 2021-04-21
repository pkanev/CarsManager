using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Models.Issues;
using Client.Core.Models.Liabilities;
using Client.Core.Models.Vehicles;
using Client.Core.Properties;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
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

        public int BeltMileageWarnings => Settings.Default.EnableBeltNotifications && Issues != null ? Issues.BeltMileageWarnings : 0;
        public int BeltMileageAlerts => Settings.Default.EnableBeltNotifications && Issues != null ? Issues.BeltMileageAlerts : 0;
        public int BrakeLiningsMileageWarnings => Settings.Default.EnableBrakeLiningsNotifications && Issues != null ? Issues.BrakeLiningsMileageWarnings : 0;
        public int BrakeLiningsMileageAlerts => Settings.Default.EnableBrakeLiningsNotifications && Issues != null ? Issues.BrakeLiningsMileageAlerts : 0;
        public int BrakeDisksMileageWarnings => Settings.Default.EnableBrakeDisksNotifications && Issues != null ? Issues.BrakeDisksMileageWarnings : 0;
        public int BrakeDisksMileageAlerts => Settings.Default.EnableBrakeDisksNotifications && Issues != null ? Issues.BrakeDisksMileageAlerts : 0;
        public int CoolantMileageWarnings => Settings.Default.EnableCoolantNotifications && Issues != null ? Issues.CoolantMileageWarnings : 0;
        public int CoolantMileageAlerts => Settings.Default.EnableCoolantNotifications && Issues != null ? Issues.CoolantMileageAlerts : 0;
        public int OilMileageWarnings => Settings.Default.EnableOilNotifications && Issues != null ? Issues.OilMileageWarnings : 0;
        public int OilMileageAlerts => Settings.Default.EnableOilNotifications && Issues != null ? Issues.OilMileageAlerts : 0;
        public int MotWarnings => Settings.Default.EnableMotNotifications && Issues != null ? Issues.MotWarnings : 0;
        public int MotAlerts => Settings.Default.EnableMotNotifications && Issues != null ? Issues.MotAlerts : 0;
        public int CivilLiabilityWarnings => Settings.Default.EnableCivilLiabilityNotifications && Issues != null ? Issues.CivilLiabilityWarnings : 0;
        public int CivilLiabilityAlerts => Settings.Default.EnableCivilLiabilityNotifications && Issues != null ? Issues.CivilLiabilityAlerts : 0;
        public int CarInsuranceWarnings => Settings.Default.EnableCarInsuranceNotifications && Issues != null ? Issues.CarInsuranceWarnings : 0;
        public int CarInsuranceAlerts => Settings.Default.EnableCarInsuranceNotifications && Issues != null ? Issues.CarInsuranceAlerts : 0;
        public int VignetteWarnings => Settings.Default.EnableVignetteNotifications && Issues != null ? Issues.VignetteWarnings : 0;
        public int VignetteAlerts => Settings.Default.EnableVignetteNotifications && Issues != null ? Issues.VignetteAlerts : 0;
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

        public HomeViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
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
        }

        public override async Task Initialize()
        {
            await GetIssues();
            await GetVehicleSummary();
        }

        private async Task GetIssues()
        {
            var parameters = new Dictionary<string, int>();
            parameters["BeltW"] = Settings.Default.BeltMileageWarningLimit;
            parameters["BeltA"] = Settings.Default.BeltMileageAlertLimit;
            parameters["BeltC"] = Settings.Default.BeltChangeMileage;
            parameters["BrakeLiningsW"] = Settings.Default.BrakeLiningsMileageWarningLimit;
            parameters["BrakeLiningsA"] = Settings.Default.BrakeLiningsMileageAlertLimit;
            parameters["BrakeLiningsC"] = Settings.Default.BrakeLiningsChangeMileage;
            parameters["BrakeDisksW"] = Settings.Default.BrakeDisksMileageWarningLimit;
            parameters["BrakeDisksA"] = Settings.Default.BrakeDisksMileageAlertLimit;
            parameters["BrakeDisksC"] = Settings.Default.BrakeDisksChangeMileage;
            parameters["CoolantW"] = Settings.Default.CoolantMileageWarningLimit;
            parameters["CoolantA"] = Settings.Default.CoolantMileageAlertLimit;
            parameters["CoolantC"] = Settings.Default.CoolantChangeMileage;
            parameters["OilW"] = Settings.Default.OilMileageWarningLimit;
            parameters["OilA"] = Settings.Default.OilMileageAlertLimit;
            parameters["OilC"] = Settings.Default.OilChangeMileage;
            parameters["MotW"] = Settings.Default.MotWarningLimit;
            parameters["MotA"] = Settings.Default.MotAlertLimit;
            parameters["CivilLiabilityW"] = Settings.Default.CivilLiabilityWarningLimit;
            parameters["CivilLiabilityA"] = Settings.Default.CivilLiabilityAlertLimit;
            parameters["CarInsuranceW"] = Settings.Default.CarInsuranceWarningLimit;
            parameters["CarInsuranceA"] = Settings.Default.CarInsuranceAlertLimit;
            parameters["VignetteW"] = Settings.Default.VignetteWarningLimit;
            parameters["VignetteA"] = Settings.Default.VignetteAlertLimit;

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
    }
}
