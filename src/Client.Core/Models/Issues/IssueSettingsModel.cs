using Client.Core.Properties;

namespace Client.Core.Models.Issues
{
    public class IssueSettingsModel
    {
        public int BeltMileageWarningLimit => Settings.Default.BeltMileageWarningLimit;
        public int BeltMileageAlertLimit => Properties.Settings.Default.BeltMileageAlertLimit;
        public int BrakeLiningsMileageWarningLimit => Properties.Settings.Default.BrakeLiningsMileageWarningLimit;
        public int BrakeLiningsMileageAlertLimit => Properties.Settings.Default.BrakeLiningsMileageAlertLimit;
        public int BrakeDisksMileageWarningLimit => Properties.Settings.Default.BrakeDisksMileageWarningLimit;
        public int BrakeDisksMileageAlertLimit => Properties.Settings.Default.BrakeDisksMileageAlertLimit;
        public int CoolantMileageWarningLimit => Properties.Settings.Default.CoolantMileageWarningLimit;
        public int CoolantMileageAlertLimit => Properties.Settings.Default.CoolantMileageAlertLimit;
        public int OilMileageWarningLimit => Properties.Settings.Default.OilMileageWarningLimit;
        public int OilMileageAlertLimit => Properties.Settings.Default.OilMileageAlertLimit;
        public int MotWarningLimit => Properties.Settings.Default.MotWarningLimit;
        public int MotAlertLimit => Properties.Settings.Default.MotAlertLimit;
        public int CivilLiabilityWarningLimit => Properties.Settings.Default.CivilLiabilityWarningLimit;
        public int CivilLiabilityAlertLimit => Properties.Settings.Default.CivilLiabilityAlertLimit;
        public int CarInsuranceWarningLimit => Properties.Settings.Default.CarInsuranceWarningLimit;
        public int CarInsuranceAlertLimit => Properties.Settings.Default.CarInsuranceAlertLimit;
        public int VignetteWarningsimit => Properties.Settings.Default.VignetteWarningLimit;
        public int VignetteAlertLimit => Properties.Settings.Default.VignetteAlertLimit;
    }
}
