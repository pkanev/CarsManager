namespace Client.Core.Data
{
    public static class Settings
    {
        public static int OilMileageAlertLimit => Properties.Settings.Default.OilMileageAlertLimit;
        public static int OilMileageWarningLimit => Properties.Settings.Default.OilMileageWarningLimit;
        public static int OilChangeMileage => Properties.Settings.Default.OilChangeMileage;

        public static int BeltMileageAlertLimit => Properties.Settings.Default.BeltMileageAlertLimit;
        public static int BeltMileageWarningLimit => Properties.Settings.Default.BeltMileageWarningLimit;
        public static int BeltChangeMileage => Properties.Settings.Default.BeltChangeMileage;

        public static int BrakeDisksMileageAlertLimit => Properties.Settings.Default.BrakeDisksMileageAlertLimit;
        public static int BrakeDisksMileageWarningLimit => Properties.Settings.Default.BrakeDisksMileageWarningLimit;
        public static int BrakeDisksChangeMileage => Properties.Settings.Default.BrakeDisksChangeMileage;

        public static int BrakeLiningsMileageAlertLimit => Properties.Settings.Default.BrakeLiningsMileageAlertLimit;
        public static int BrakeLiningsMileageWarningLimit => Properties.Settings.Default.BrakeLiningsMileageWarningLimit;
        public static int BrakeLiningsChangeMileage => Properties.Settings.Default.BrakeLiningsChangeMileage;

        public static int CoolantMileageAlertLimit => Properties.Settings.Default.CoolantMileageAlertLimit;
        public static int CoolantMileageWarningLimit => Properties.Settings.Default.CoolantMileageWarningLimit;
        public static int CoolantChangeMileage => Properties.Settings.Default.CoolantChangeMileage;

        public static int MotAlertLimit => Properties.Settings.Default.MotAlertLimit;
        public static int MotWarningLimit => Properties.Settings.Default.MotWarningLimit;

        public static int CivilLiabilityAlertLimit => Properties.Settings.Default.CivilLiabilityAlertLimit;
        public static int CivilLiabilityWarningLimit => Properties.Settings.Default.CivilLiabilityWarningLimit;

        public static int CarInsuranceAlertLimit => Properties.Settings.Default.CarInsuranceAlertLimit;
        public static int CarInsuranceWarningLimit => Properties.Settings.Default.CarInsuranceWarningLimit;

        public static int VignetteAlertLimit => Properties.Settings.Default.VignetteAlertLimit;
        public static int VignetteWarningLimit => Properties.Settings.Default.VignetteWarningLimit;

        public static bool EnableBeltNotifications => Properties.Settings.Default.EnableBeltNotifications;
        public static bool EnableBrakeDisksNotifications => Properties.Settings.Default.EnableBrakeDisksNotifications;
        public static bool EnableBrakeLiningsNotifications => Properties.Settings.Default.EnableBrakeLiningsNotifications;
        public static bool EnableCarInsuranceNotifications => Properties.Settings.Default.EnableCarInsuranceNotifications;
        public static bool EnableCivilLiabilityNotifications => Properties.Settings.Default.EnableCivilLiabilityNotifications;
        public static bool EnableCoolantNotifications => Properties.Settings.Default.EnableCoolantNotifications;
        public static bool EnableMotNotifications => Properties.Settings.Default.EnableMotNotifications;
        public static bool EnableOilNotifications => Properties.Settings.Default.EnableOilNotifications;
        public static bool EnableVignetteNotifications => Properties.Settings.Default.EnableVignetteNotifications;

    }
}
