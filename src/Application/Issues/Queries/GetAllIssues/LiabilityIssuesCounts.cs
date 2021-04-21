namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public class LiabilityIssuesCounts
    {
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
