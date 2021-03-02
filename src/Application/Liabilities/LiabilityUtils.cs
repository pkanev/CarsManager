using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Liabilities
{
    public class LiabilityUtils : ILiabilityUtils
    {
        public async Task<Liability> FindLiabilityAsync(int id, LiabilityType liability, IApplicationDbContext context) => liability switch
        {
            LiabilityType.MOT => await context.MOTs.FindAsync(id),
            LiabilityType.CivilLiability => await context.CivilLiabilities.FindAsync(id),
            LiabilityType.CarInsurance => await context.CarInsurances.FindAsync(id),
            LiabilityType.Vignette => await context.Vignettes.FindAsync(id),
            _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}")
        };

        public string GetLiabilityName(LiabilityType liability) => liability switch
        {
            LiabilityType.MOT => nameof(MOT),
            LiabilityType.CivilLiability => nameof(CivilLiability),
            LiabilityType.CarInsurance => nameof(CarInsurance),
            LiabilityType.Vignette => nameof(Vignette),
            _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}")
        };
    }
}
