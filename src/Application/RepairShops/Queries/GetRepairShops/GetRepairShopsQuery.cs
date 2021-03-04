using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.RepairShops.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.RepairShops.Queries.GetRepairShops
{
    public class GetRepairShopsQuery : IRequest<RepairShopsVm>
    {
    }

    public class GetRepairShopsQueryHandler : IRequestHandler<GetRepairShopsQuery, RepairShopsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRepairShopsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RepairShopsVm> Handle(
            GetRepairShopsQuery request,
            CancellationToken cancellationToken) => new RepairShopsVm
            {
                RepairShops = await context.RepairShops
                    .ProjectTo<RepairShopDto>(mapper.ConfigurationProvider)
                    .OrderBy(r => r.Name)
                    .ToListAsync()
            };
    }
}
