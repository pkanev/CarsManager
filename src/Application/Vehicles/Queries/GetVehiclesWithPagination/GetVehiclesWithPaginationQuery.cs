﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination
{
    [Authorise]
    public class GetVehiclesWithPaginationQuery : IRequest<PaginatedList<ListedVehicleDto>>
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }

    public class GetVehiclesWithPaginationQueryHandler : IRequestHandler<GetVehiclesWithPaginationQuery, PaginatedList<ListedVehicleDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetVehiclesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<ListedVehicleDto>> Handle(
            GetVehiclesWithPaginationQuery request,
            CancellationToken cancellationToken)
            => await context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .OrderBy(v => v.LicencePlate)
                .ProjectTo<ListedVehicleDto>(mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
