using Client.Core.Data;
using Client.Core.Models.Issues;

namespace Client.Core.Models.Liabilities
{
    public class LiabilityForReportModel : LiabilityExtendedModel
    {
        public string Color { get; set; }
        public int VehicleType { get; set; }
        public string VehicleTypeName { get; set; }
        public int MakeId { get; set; }
        public string Make { get; set; }
        public int ModelId { get; set; }
        public string Model { get; set; }
        public int RemainingDays { get; set; }
        public string MakeAndModel => $"{Make} {Model}";
        public IssueLevel IssueLevel => LiabilityType switch
        {
            LiabilityType.MOT => GetIssueLevel(Settings.MotWarningLimit, Settings.MotAlertLimit, Settings.EnableMotNotifications),
            LiabilityType.CivilLiability => GetIssueLevel(Settings.CivilLiabilityWarningLimit, Settings.CivilLiabilityAlertLimit, Settings.EnableCivilLiabilityNotifications),
            LiabilityType.CarInsurance => GetIssueLevel(Settings.CarInsuranceWarningLimit, Settings.CarInsuranceAlertLimit, Settings.EnableCarInsuranceNotifications),
            LiabilityType.Vignette => GetIssueLevel(Settings.VignetteWarningLimit, Settings.VignetteAlertLimit, Settings.EnableVignetteNotifications),
            _ => IssueLevel.Off
        };

        private IssueLevel GetIssueLevel(int warningLimit, int alertLimit, bool isEnabled)
        {
            if (!isEnabled)
                return IssueLevel.Off;

            if (RemainingDays > warningLimit)
                return IssueLevel.Ok;

            if (RemainingDays > alertLimit)
                return IssueLevel.Warning;

            return IssueLevel.Alert;
        }
    }
}
