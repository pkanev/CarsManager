using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.RoadBookEntries.Queries.GetPaginatedRoadBook
{
    public class GetPaginatedRoadBookQuery : IRequest<PaginatedList<RoadBookFullEntryDto>>
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
        public int VehicleId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class GetPaginatedRoadBookQueryHandler : IRequestHandler<GetPaginatedRoadBookQuery, PaginatedList<RoadBookFullEntryDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetPaginatedRoadBookQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<PaginatedList<RoadBookFullEntryDto>> Handle(GetPaginatedRoadBookQuery request, CancellationToken cancellationToken)
            => await context.RoadBookEntries
                .Include(e => e.Vehicle)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(e => e.ActiveUsers)
                .Include(e => e.Employees)
                .Where(e => e.VehicleId == request.VehicleId && e.CheckedOut >= request.From && e.CheckedOut < request.To)
                .OrderBy(e => e.CheckedOut)
                .ProjectTo<RoadBookFullEntryDto>(mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
