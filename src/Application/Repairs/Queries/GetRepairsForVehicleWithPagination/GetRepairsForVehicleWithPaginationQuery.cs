using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Repairs.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Repairs.Queries.GetRepairsForVehicleWithPagination
{
    [Authorise]
    public class GetRepairsForVehicleWithPaginationQuery : IRequest<PaginatedList<BasicRepairForVehicleDto>>, IMapFrom<GetRepairsForVehicleWithPaginationQueryDto>
    {
        public int VehicleId { get; set; }
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }

    public class GetRepairsForVehicleWithPaginationQueryHandler : IRequestHandler<GetRepairsForVehicleWithPaginationQuery, PaginatedList<BasicRepairForVehicleDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRepairsForVehicleWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<BasicRepairForVehicleDto>> Handle(
            GetRepairsForVehicleWithPaginationQuery request,
            CancellationToken cancellationToken) => await context.Repairs
                .Include(r => r.RepairShop)
                .Where(r => r.VehicleId == request.VehicleId)
                .OrderByDescending(r => r.Date)
                .ProjectTo<BasicRepairForVehicleDto>(mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
