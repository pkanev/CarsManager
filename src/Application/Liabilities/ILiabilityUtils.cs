using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReport;
using CarsManager.Domain.Entities;

namespace CarsManager.Application.Liabilities
{
    public interface ILiabilityUtils
    {
        Task<Liability> FindLiabilityAsync(int id, LiabilityType liability, IApplicationDbContext context);

        string GetLiabilityName(LiabilityType liability);

        Task<List<LiabilityForReportDto>> GetAllLiabilitiesForReport(LiabilityType liability, IApplicationDbContext context, IMapper mapper);
    }
}