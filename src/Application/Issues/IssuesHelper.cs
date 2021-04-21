using CarsManager.Domain.Entities;

namespace CarsManager.Application.Issues
{
    public class IssuesHelper : IIssuesHelper
    {
        public int GetMileageAtRepair(RepairIssueType issueType, Vehicle vehicle) => issueType switch
        {
            RepairIssueType.BeltMileage => vehicle.BeltMileage,
            RepairIssueType.BrakeDisksMileage => vehicle.BrakeDisksMileage,
            RepairIssueType.BrakeLiningsMileage => vehicle.BrakeLiningsMileage,
            RepairIssueType.CoolantMileage => vehicle.CoolantMileage,
            RepairIssueType.OilMileage => vehicle.OilMileage,
            _ => 0
        };
    }
}
