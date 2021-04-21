using Client.Core.Data;

namespace Client.Core.Models.Issues
{
    public class IssueModel
    {
        public string LicencePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int VehicleType { get; set; }
        public string VehicleTypeName { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public int MileageAtRepair { get; set; }
        public string MakeAndModel => $"{Make} {Model}";
        public IssueType IssueType { get; set; }
        public int Remainder => IssueType switch
        {
            IssueType.BeltMileage => Settings.BeltChangeMileage + MileageAtRepair - Mileage,
            IssueType.BrakeDisksMileage => Settings.BrakeDisksChangeMileage + MileageAtRepair - Mileage,
            IssueType.BrakeLiningsMileage => Settings.BrakeLiningsChangeMileage + MileageAtRepair - Mileage,
            IssueType.CoolantMileage => Settings.CoolantChangeMileage + MileageAtRepair - Mileage,
            IssueType.OilMileage => Settings.OilChangeMileage + MileageAtRepair - Mileage,
            _ => 0
        };

        public IssueLevel IssueLevel => IssueType switch
        {
            IssueType.BeltMileage => GetIssueLevel(Settings.BeltMileageWarningLimit, Settings.BeltMileageAlertLimit, Settings.EnableBeltNotifications),
            IssueType.BrakeDisksMileage => GetIssueLevel(Settings.BrakeDisksMileageWarningLimit, Settings.BrakeDisksMileageAlertLimit, Settings.EnableBrakeDisksNotifications),
            IssueType.BrakeLiningsMileage => GetIssueLevel(Settings.BrakeLiningsMileageWarningLimit, Settings.BrakeLiningsMileageAlertLimit, Settings.EnableBrakeLiningsNotifications),
            IssueType.CoolantMileage => GetIssueLevel(Settings.CoolantMileageWarningLimit, Settings.CoolantMileageAlertLimit, Settings.EnableCoolantNotifications),
            IssueType.OilMileage => GetIssueLevel(Settings.OilMileageWarningLimit, Settings.OilMileageAlertLimit, Settings.EnableOilNotifications),
            _ => IssueLevel.Off
        };

        private IssueLevel GetIssueLevel(int warningLimit, int alertLimit, bool isEnabled)
        {
            if (!isEnabled)
                return IssueLevel.Off;

            if (Remainder > warningLimit)
                return IssueLevel.Ok;

            if (Remainder > alertLimit)
                return IssueLevel.Warning;

            return IssueLevel.Alert;
        }
    }
}
