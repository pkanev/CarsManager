using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReport;
using CarsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<LiabilityForReportDto>> GetAllLiabilitiesForReport(LiabilityType liability, IApplicationDbContext context, IMapper mapper)
            => liability switch
            {
                LiabilityType.MOT => await context.MOTs
                    .Include(l => l.Vehicle)
                    .ThenInclude(v => v.Model)
                    .ThenInclude(m => m.Make)
                    .OrderBy(l => l.Vehicle.LicencePlate)
                    .ThenByDescending(l => l.EndDate)
                    .ProjectTo<LiabilityForReportDto>(mapper.ConfigurationProvider)
                    .ToListAsync(),

                LiabilityType.CivilLiability => await context.CivilLiabilities
                    .Include(l => l.Vehicle)
                    .ThenInclude(v => v.Model)
                    .ThenInclude(m => m.Make)
                    .OrderBy(l => l.Vehicle.LicencePlate)
                    .ThenByDescending(l => l.EndDate)
                    .ProjectTo<LiabilityForReportDto>(mapper.ConfigurationProvider)
                    .ToListAsync(),

                LiabilityType.CarInsurance => await context.CarInsurances
                    .Include(l => l.Vehicle)
                    .ThenInclude(v => v.Model)
                    .ThenInclude(m => m.Make)
                    .OrderBy(l => l.Vehicle.LicencePlate)
                    .ThenByDescending(l => l.EndDate)
                    .ProjectTo<LiabilityForReportDto>(mapper.ConfigurationProvider)
                    .ToListAsync(),

                LiabilityType.Vignette => await context.Vignettes
                    .Include(l => l.Vehicle)
                    .ThenInclude(v => v.Model)
                    .ThenInclude(m => m.Make)
                    .OrderBy(l => l.Vehicle.LicencePlate)
                    .ThenByDescending(l => l.EndDate)
                    .ProjectTo<LiabilityForReportDto>(mapper.ConfigurationProvider)
                    .ToListAsync(),

                _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}")
            };
    }
}
