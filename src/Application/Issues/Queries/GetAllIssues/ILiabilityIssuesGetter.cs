using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;

namespace CarsManager.Application.Issues.Queries.GetAllIssues
{
    public interface ILiabilityIssuesGetter
    {
        Task<LiabilityIssuesCounts> GetLiabilityIssuesCounts(IApplicationDbContext context, RepairIssuesLimitsDto limits);
    }
}