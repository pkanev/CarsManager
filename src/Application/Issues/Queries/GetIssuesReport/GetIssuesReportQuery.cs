using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Issues.Queries.GetIssuesReport
{
    [Authorise]
    public class GetIssuesReportQuery : IRequest<IList<IssueReportDto>>
    {
        public RepairIssueType IssueType { get; set; }
    }

    public class GetBeltsReportQueryHandler : IRequestHandler<GetIssuesReportQuery, IList<IssueReportDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIssuesHelper issuesHelper;

        public GetBeltsReportQueryHandler(IApplicationDbContext context, IIssuesHelper issuesHelper)
        {
            this.context = context;
            this.issuesHelper = issuesHelper;
        }

        public async Task<IList<IssueReportDto>> Handle(GetIssuesReportQuery request, CancellationToken cancellationToken)
            => await context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Select(v => new IssueReportDto
                {
                    LicencePlate = v.LicencePlate,
                    Color = v.Color,
                    Make = v.Model.Make.Name,
                    Model = v.Model.Name,
                    VehicleType = v.Model.VehicleType,
                    Mileage = v.Mileage,
                    MileageAtRepair = issuesHelper.GetMileageAtRepair(request.IssueType, v),
                    IssueType = (int)request.IssueType
                })
                .ToListAsync();
    }
}
