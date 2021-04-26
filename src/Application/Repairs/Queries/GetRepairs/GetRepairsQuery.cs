using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Repairs.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Repairs.Queries.GetRepairs
{
    [Authorise]
    public class GetRepairsQuery : IRequest<RepairsVm>
    {
        public int VehicleId { get; set; }
    }

    public class GetRepairsQueryHandler : IRequestHandler<GetRepairsQuery, RepairsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRepairsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RepairsVm> Handle(
            GetRepairsQuery request,
            CancellationToken cancellationToken)
            => new RepairsVm
                {
                    Repairs = await context.Repairs
                    .Include(r => r.Vehicle)
                    .Include(r => r.RepairShop)
                    .Where(r => r.VehicleId == request.VehicleId)
                    .OrderByDescending(r => r.Date)
                    .ProjectTo<RepairDto>(mapper.ConfigurationProvider)
                    .ToListAsync()
                };
    }
}
