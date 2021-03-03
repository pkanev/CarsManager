using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Repairs.Queries.Dtos;
using CarsManager.Application.RepairShops.Queries.Dtos;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.RepairShops.Queries.GetRepairShop
{
    public class GetRepairShopQuery : IRequest<RepairShopVm>
    {
        public int Id { get; set; }
    }

    public class GetRepairShopQueryHandler : IRequestHandler<GetRepairShopQuery, RepairShopVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRepairShopQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RepairShopVm> Handle(GetRepairShopQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.RepairShops
                .Include(r => r.Repairs)
                .ThenInclude(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(RepairShop), request.Id);

            return new RepairShopVm
            {
                RepairShop = mapper.Map<RepairShopDto>(entity),
                Repairs = entity.Repairs.Select(r => mapper.Map<RepairDto>(r)).ToList()
            };
        }
    }
}
