using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.RoadBookEntries.Queries.GetRoadBook
{
    [Authorise]
    public class GetRoadBookQuery : IRequest<IList<RoadBookFullEntryDto>>
    {
        public int VehicleId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class GetRoadBookQueryHandler : IRequestHandler<GetRoadBookQuery, IList<RoadBookFullEntryDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRoadBookQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<RoadBookFullEntryDto>> Handle(GetRoadBookQuery request, CancellationToken cancellationToken)
        => await context.RoadBookEntries
                .Include(e => e.Vehicle)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(e => e.ActiveUsers)
                .Include(e => e.Employees)
                .Where(e => e.VehicleId == request.VehicleId && e.CheckedOut >= request.From && e.CheckedOut < request.To)
                .OrderBy(e => e.CheckedOut)
                .ProjectTo<RoadBookFullEntryDto>(mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
