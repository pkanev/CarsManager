using CarsManager.Domain.Entities;

namespace CarsManager.Application.Issues
{
    public interface IIssuesHelper
    {
        int GetMileageAtRepair(RepairIssueType issueType, Vehicle vehicle);
    }
}
