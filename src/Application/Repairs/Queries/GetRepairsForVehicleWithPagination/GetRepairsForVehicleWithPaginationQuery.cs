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

namespace CarsManager.Application.Repairs.Queries.GetRepairsForVehicleWithPagination
{
    public class GetRepairsForVehicleWithPaginationQuery : IRequest<PaginatedList<ListedRepairForVehicleDto>>, IMapFrom<GetRepairsForVehicleWithPaginationQueryDto>
    {
        public int VehicleId { get; set; }
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }

    public class GetRepairsForVehicleWithPaginationQueryHandler : IRequestHandler<GetRepairsForVehicleWithPaginationQuery, PaginatedList<ListedRepairForVehicleDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRepairsForVehicleWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<ListedRepairForVehicleDto>> Handle(
            GetRepairsForVehicleWithPaginationQuery request,
            CancellationToken cancellationToken) => await context.Repairs
                .Include(r => r.RepairShop)
                .Where(r => r.VehicleId == request.VehicleId)
                .OrderByDescending(r => r.Date)
                .ProjectTo<ListedRepairForVehicleDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
