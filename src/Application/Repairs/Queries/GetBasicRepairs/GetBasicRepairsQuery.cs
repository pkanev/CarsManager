using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Repairs.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Repairs.Queries.GetBasicRepairs
{
    public class GetBasicRepairsQuery : IRequest<IList<BasicRepairForVehicleDto>>
    {
        public int VehicleId { get; set; }
    }

    public class GetBasicRepairsQueryHandler : IRequestHandler<GetBasicRepairsQuery, IList<BasicRepairForVehicleDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetBasicRepairsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<BasicRepairForVehicleDto>> Handle(GetBasicRepairsQuery request, CancellationToken cancellationToken)
            => await context.Repairs
                    .Include(r => r.RepairShop)
                    .Where(r => r.VehicleId == request.VehicleId)
                    .OrderByDescending(r => r.Date)
                    .ProjectTo<BasicRepairForVehicleDto>(mapper.ConfigurationProvider)
                    .ToListAsync();
    }
}
