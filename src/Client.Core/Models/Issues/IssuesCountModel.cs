namespace Client.Core.Models.Issues
{
    public class IssuesCountModel
    {
        public int BeltMileageWarnings { get; set; }
        public int BeltMileageAlerts { get; set; }
        public int BrakeLiningsMileageWarnings { get; set; }
        public int BrakeLiningsMileageAlerts { get; set; }
        public int BrakeDisksMileageWarnings { get; set; }
        public int BrakeDisksMileageAlerts { get; set; }
        public int CoolantMileageWarnings { get; set; }
        public int CoolantMileageAlerts { get; set; }
        public int OilMileageWarnings { get; set; }
        public int OilMileageAlerts { get; set; }
        public int MotWarnings { get; set; }
        public int MotAlerts { get; set; }
        public int CivilLiabilityWarnings { get; set; }
        public int CivilLiabilityAlerts { get; set; }
        public int CarInsuranceWarnings { get; set; }
        public int CarInsuranceAlerts { get; set; }
        public int VignetteWarnings { get; set; }
        public int VignetteAlerts { get; set; }
    }
}
