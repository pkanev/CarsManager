using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Liabilities
{
    public interface ILiabilityUtils
    {
        Task<Liability> FindLiabilityAsync(int id, LiabilityType liability, IApplicationDbContext context);

        string GetLiabilityName(LiabilityType liability);
    }
}