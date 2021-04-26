using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Dtos;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Issues.Queries.GetIssuesWithPagination
{
    [Authorise]
    public class GetIssuesWithPaginationQuery : IRequest<PaginatedList<IssueReportDto>>, IMapFrom<PaginationDto>
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
        public RepairIssueType IssueType { get; set; }
    }

    public class GetIssuesWithPaginationQueryHandler : IRequestHandler<GetIssuesWithPaginationQuery, PaginatedList<IssueReportDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IIssuesHelper issuesHelper;

        public GetIssuesWithPaginationQueryHandler(IApplicationDbContext context, IIssuesHelper issuesHelper)
        {
            this.context = context;
            this.issuesHelper = issuesHelper;
        }

        public async Task<PaginatedList<IssueReportDto>> Handle(GetIssuesWithPaginationQuery request, CancellationToken cancellationToken)
        =>  await context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .OrderBy(v => v.LicencePlate)
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
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
