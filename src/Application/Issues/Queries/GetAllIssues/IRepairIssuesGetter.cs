using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;

namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public interface IRepairIssuesGetter
    {
        Task<int> GetBeltMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit);
        Task<int> GetBeltMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit);
        Task<int> GetOilMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit);
        Task<int> GetOilMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit);
        Task<int> GetBrakeLiningsMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit);
        Task<int> GetBrakeLiningsMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit);
        Task<int> GetBrakeDisksMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit);
        Task<int> GetBrakeDisksMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit);
        Task<int> GetCoolantMileageAlerts(IApplicationDbContext context, int allowedMileage, int alertLimit);
        Task<int> GetCoolantMileageWarnings(IApplicationDbContext context, int allowedMileage, int warningLimit, int alertLimit);
    }
}