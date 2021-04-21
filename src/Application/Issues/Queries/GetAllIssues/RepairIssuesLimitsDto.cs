namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public class RepairIssuesLimitsDto
    {
        public int MotWarningLimit { get; set; }
        public int MotAlertLimit { get; set; }
        public int CivilLiabilityWarningLimit { get; set; }
        public int CivilLiabilityAlertLimit { get; set; }
        public int CarInsuranceWarningLimit { get; set; }
        public int CarInsuranceAlertLimit { get; set; }
        public int VignetteWarningLimit { get; set; }
        public int VignetteAlertLimit { get; set; }
    }
}
