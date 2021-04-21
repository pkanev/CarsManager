using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Interfaces;
using MediatR;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReport
{
    public class GetLiabilitiesForReportQuery : IRequest<IList<LiabilityForReportDto>>
    {
        public LiabilityType Liability { get; set; }
    }

    public class GetLiabilitiesForReportQueryHandler : IRequestHandler<GetLiabilitiesForReportQuery, IList<LiabilityForReportDto>>
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

        public async Task<IList<LiabilityForReportDto>> Handle(
            GetLiabilitiesForReportQuery request,
            CancellationToken cancellationToken)
        {
            var result = new Dictionary<string, LiabilityForReportDto>();

            var liabilities = await liabilityUtils.GetAllLiabilitiesForReport(request.Liability, context, mapper);
            liabilities.ForEach(l =>
            {
                if (!result.ContainsKey(l.LicencePlate))
                    result[l.LicencePlate] = l;
            });

            return result.Values.ToList();
        }
    }
}
