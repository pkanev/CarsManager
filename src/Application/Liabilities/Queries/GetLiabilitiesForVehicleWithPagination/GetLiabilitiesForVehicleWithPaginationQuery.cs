using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using MediatR;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicleWithPagination
{
    public class GetLiabilitiesForVehicleWithPaginationQuery : IRequest<PaginatedList<ListedLiabilityDto>>, IMapFrom<GetLiabilitiesForVehicleWithPaginationQueryDto>
    {
        public int VehicleId { get; set; }
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
        public LiabilityType Liability { get; set; }
    }

    public class GetLiabilitiesForVehicleWithPaginationQueryHandler
        : IRequestHandler<GetLiabilitiesForVehicleWithPaginationQuery, PaginatedList<ListedLiabilityDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetLiabilitiesForVehicleWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<ListedLiabilityDto>> Handle(
            GetLiabilitiesForVehicleWithPaginationQuery request,
            CancellationToken cancellationToken) => request.Liability switch
            {
                LiabilityType.MOT => await context.MOTs
                    .Where(l => l.VehicleId == request.VehicleId)
                    .OrderByDescending(l => l.Date)
                    .ProjectTo<ListedLiabilityDto>(mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize),

                LiabilityType.CivilLiability => await context.CivilLiabilities
                    .Where(l => l.VehicleId == request.VehicleId)
                    .OrderByDescending(l => l.Date)
                    .ProjectTo<ListedLiabilityDto>(mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize),

                LiabilityType.CarInsurance => await context.CarInsurances
                    .Where(l => l.VehicleId == request.VehicleId)
                    .OrderByDescending(l => l.Date)
                    .ProjectTo<ListedLiabilityDto>(mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize),

                LiabilityType.Vignette => await context.Vignettes
                    .Where(l => l.VehicleId == request.VehicleId)
                    .OrderByDescending(l => l.Date)
                    .ProjectTo<ListedLiabilityDto>(mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize),

                _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {request.Liability}")
            };
    }
}
