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

namespace CarsManager.Application.MOTs.Queries.GetMOTsForVehicleWithPagination
{
    public class GetMOTsForVehicleWithPaginationQuery : IRequest<PaginatedList<ListedMOTDto>>
    {
        public int VehicleId { get; set; }
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }

    public class GetMOTsForVehicleWithPaginationQueryHandler : IRequestHandler<GetMOTsForVehicleWithPaginationQuery, PaginatedList<ListedMOTDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMOTsForVehicleWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<ListedMOTDto>> Handle(GetMOTsForVehicleWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await context.MOTs
                .OrderByDescending(m => m.Date)
                .ProjectTo<ListedMOTDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
