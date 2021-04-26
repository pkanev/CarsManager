using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Dtos;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReport;
using MediatR;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReportWithPagination
{
    [Authorise]
    public class GetLiabilitiesForReportWithPaginationQuery : IRequest<PaginatedList<LiabilityForReportDto>>, IMapFrom<PaginationDto>
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
        public LiabilityType Liability { get; set; }
    }

    public class GetLiabilitiesForReportQueryHandler : IRequestHandler<GetLiabilitiesForReportWithPaginationQuery, PaginatedList<LiabilityForReportDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILiabilityUtils liabilityUtils;

        public GetLiabilitiesForReportQueryHandler(IApplicationDbContext context, IMapper mapper, ILiabilityUtils liabilityUtils)
        {
            this.context = context;
            this.mapper = mapper;
            this.liabilityUtils = liabilityUtils;
        }

        public async Task<PaginatedList<LiabilityForReportDto>> Handle(
            GetLiabilitiesForReportWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            var result = new Dictionary<string, LiabilityForReportDto>();

            var liabilities = await liabilityUtils.GetAllLiabilitiesForReport(request.Liability, context, mapper);
            liabilities.ForEach(l =>
            {
                if (!result.ContainsKey(l.LicencePlate))
                    result[l.LicencePlate] = l;
            });

            return result.Values.AsQueryable().ToPaginatedList(request.PageNumber, request.PageSize);
        }
    }
}
