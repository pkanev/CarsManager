using Client.Core.Rest;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class SettingsViewModel : SubPageViewModel
    {
        public decimal Vat
        {
            get => Properties.Settings.Default.VAT;
            set
            {
                Properties.Settings.Default.VAT = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => Vat);
            }
        }

        public string ServerAddress
        {
            get => Properties.Settings.Default.ApiAddress;
            set
            {
                Properties.Settings.Default.ApiAddress = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => ServerAddress);
            }
        }

        public int MotWarningLimit
        {
            get => Properties.Settings.Default.MotWarningLimit;
            set
            {
                Properties.Settings.Default.MotWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => MotWarningLimit);
            }
        }

        public int MotAlertLimit
        {
            get => Properties.Settings.Default.MotAlertLimit;
            set
            {
                Properties.Settings.Default.MotAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => MotAlertLimit);
            }
        }

        public int CivilLiabilityWarningLimit
        {
            get => Properties.Settings.Default.CivilLiabilityWarningLimit;
            set
            {
                Properties.Settings.Default.CivilLiabilityWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CivilLiabilityWarningLimit);
            }
        }

        public int CivilLiabilityAlertLimit
        {
            get => Properties.Settings.Default.CivilLiabilityAlertLimit;
            set
            {
                Properties.Settings.Default.CivilLiabilityAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CivilLiabilityAlertLimit);
            }
        }

        public int CarInsuranceWarningLimit
        {
            get => Properties.Settings.Default.CarInsuranceWarningLimit;
            set
            {
                Properties.Settings.Default.CarInsuranceWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CarInsuranceWarningLimit);
            }
        }

        public int CarInsuranceAlertLimit
        {
            get => Properties.Settings.Default.CarInsuranceAlertLimit;
            set
            {
                Properties.Settings.Default.CarInsuranceAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CarInsuranceAlertLimit);
            }
        }

        public int VignetteWarningLimit
        {
            get => Properties.Settings.Default.VignetteWarningLimit;
            set
            {
                Properties.Settings.Default.VignetteWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => VignetteWarningLimit);
            }
        }

        public int VignetteAlertLimit
        {
            get => Properties.Settings.Default.VignetteAlertLimit;
            set
            {
                Properties.Settings.Default.VignetteAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => VignetteAlertLimit);
            }
        }

        public int BeltChangeMileage
        {
            get => Properties.Settings.Default.BeltChangeMileage;
            set
            {
                Properties.Settings.Default.BeltChangeMileage = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BeltChangeMileage);
            }
        }

        public int BeltMileageWarningLimit
        {
            get => Properties.Settings.Default.BeltMileageWarningLimit;
            set
            {
                Properties.Settings.Default.BeltMileageWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BeltMileageWarningLimit);
            }
        }

        public int BeltMileageAlertLimit
        {
            get => Properties.Settings.Default.BeltMileageAlertLimit;
            set
            {
                Properties.Settings.Default.BeltMileageAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BeltMileageAlertLimit);
            }
        }

        public int BrakeLiningsChangeMileage
        {
            get => Properties.Settings.Default.BrakeLiningsChangeMileage;
            set
            {
                Properties.Settings.Default.BrakeLiningsChangeMileage = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BrakeLiningsChangeMileage);
            }
        }

        public int BrakeLiningsMileageWarningLimit
        {
            get => Properties.Settings.Default.BrakeLiningsMileageWarningLimit;
            set
            {
                Properties.Settings.Default.BrakeLiningsMileageWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BrakeLiningsMileageWarningLimit);
            }
        }

        public int BrakeLiningsMileageAlertLimit
        {
            get => Properties.Settings.Default.BrakeLiningsMileageAlertLimit;
            set
            {
                Properties.Settings.Default.BrakeLiningsMileageAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BrakeLiningsMileageAlertLimit);
            }
        }

        public int BrakeDisksChangeMileage
        {
            get => Properties.Settings.Default.BrakeDisksChangeMileage;
            set
            {
                Properties.Settings.Default.BrakeDisksChangeMileage = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BrakeDisksChangeMileage);
            }
        }

        public int BrakeDisksMileageWarningLimit
        {
            get => Properties.Settings.Default.BrakeDisksMileageWarningLimit;
            set
            {
                Properties.Settings.Default.BrakeDisksMileageWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BrakeDisksMileageWarningLimit);
            }
        }

        public int BrakeDisksMileageAlertLimit
        {
            get => Properties.Settings.Default.BrakeDisksMileageAlertLimit;
            set
            {
                Properties.Settings.Default.BrakeDisksMileageAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => BrakeDisksMileageAlertLimit);
            }
        }

        public int CoolantChangeMileage
        {
            get => Properties.Settings.Default.CoolantChangeMileage;
            set
            {
                Properties.Settings.Default.CoolantChangeMileage = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CoolantChangeMileage);
            }
        }

        public int CoolantMileageWarningLimit
        {
            get => Properties.Settings.Default.CoolantMileageWarningLimit;
            set
            {
                Properties.Settings.Default.CoolantMileageWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CoolantMileageWarningLimit);
            }
        }

        public int CoolantMileageAlertLimit
        {
            get => Properties.Settings.Default.CoolantMileageAlertLimit;
            set
            {
                Properties.Settings.Default.CoolantMileageAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => CoolantMileageAlertLimit);
            }
        }

        public int OilChangeMileage
        {
            get => Properties.Settings.Default.OilChangeMileage;
            set
            {
                Properties.Settings.Default.OilChangeMileage = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => OilChangeMileage);
            }
        }

        public int OilMileageWarningLimit
        {
            get => Properties.Settings.Default.OilMileageWarningLimit;
            set
            {
                Properties.Settings.Default.OilMileageWarningLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => OilMileageWarningLimit);
            }
        }

        public int OilMileageAlertLimit
        {
            get => Properties.Settings.Default.OilMileageAlertLimit;
            set
            {
                Properties.Settings.Default.OilMileageAlertLimit = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => OilMileageAlertLimit);
            }
        }

        public bool EnableBeltNotifications
        {
            get => Properties.Settings.Default.EnableBeltNotifications;
            set
            {
                Properties.Settings.Default.EnableBeltNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableBeltNotifications);
            }
        }

        public bool EnableOilNotifications
        {
            get => Properties.Settings.Default.EnableOilNotifications;
            set
            {
                Properties.Settings.Default.EnableOilNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableOilNotifications);
            }
        }

        public bool EnableBrakeDisksNotifications
        {
            get => Properties.Settings.Default.EnableBrakeDisksNotifications;
            set
            {
                Properties.Settings.Default.EnableBrakeDisksNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableBrakeDisksNotifications);
            }
        }

        public bool EnableBrakeLiningsNotifications
        {
            get => Properties.Settings.Default.EnableBrakeLiningsNotifications;
            set
            {
                Properties.Settings.Default.EnableBrakeLiningsNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableBrakeLiningsNotifications);
            }
        }

        public bool EnableCoolantNotifications
        {
            get => Properties.Settings.Default.EnableCoolantNotifications;
            set
            {
                Properties.Settings.Default.EnableCoolantNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableCoolantNotifications);
            }
        }

        public bool EnableMotNotifications
        {
            get => Properties.Settings.Default.EnableMotNotifications;
            set
            {
                Properties.Settings.Default.EnableMotNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableMotNotifications);
            }
        }

        public bool EnableCivilLiabilityNotifications
        {
            get => Properties.Settings.Default.EnableCivilLiabilityNotifications;
            set
            {
                Properties.Settings.Default.EnableCivilLiabilityNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableCivilLiabilityNotifications);
            }
        }

        public bool EnableCarInsuranceNotifications
        {
            get => Properties.Settings.Default.EnableCarInsuranceNotifications;
            set
            {
                Properties.Settings.Default.EnableCarInsuranceNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableCarInsuranceNotifications);
            }
        }

        public bool EnableVignetteNotifications
        {
            get => Properties.Settings.Default.EnableVignetteNotifications;
            set
            {
                Properties.Settings.Default.EnableVignetteNotifications = value;
                Properties.Settings.Default.Save();
                RaisePropertyChanged(() => EnableVignetteNotifications);
            }
        }

        public SettingsViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
        }
    }
}
