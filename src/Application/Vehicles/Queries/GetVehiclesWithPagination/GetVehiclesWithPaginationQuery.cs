using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using MediatR;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination
{
    public class GetVehiclesWithPaginationQuery : IRequest<PaginatedList<VehicleDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetVehiclesWithPaginationQueryHandler : IRequestHandler<GetVehiclesWithPaginationQuery, PaginatedList<VehicleDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetVehiclesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<VehicleDto>> Handle(GetVehiclesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await context.Employees
                .OrderBy(e => e.Surname)
                .ThenBy(e => e.GivenName)
                .ProjectTo<VehicleDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
