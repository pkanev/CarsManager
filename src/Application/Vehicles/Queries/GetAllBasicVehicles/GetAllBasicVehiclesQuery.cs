using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Queries.GetAllBasicVehicles
{
    [Authorise]
    public class GetAllBasicVehiclesQuery : IRequest<IList<ListedVehicleDto>>
    {
    }

    public class GetAllBasicVehiclesQueryHandler : IRequestHandler<GetAllBasicVehiclesQuery, IList<ListedVehicleDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllBasicVehiclesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<ListedVehicleDto>> Handle(GetAllBasicVehiclesQuery request, CancellationToken cancellationToken)
            => await context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.RoadBookEntries)
                .ThenInclude(e => e.ActiveUsers)
                .OrderBy(v => v.LicencePlate)
                .ProjectTo<ListedVehicleDto>(mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
