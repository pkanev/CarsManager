using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Repairs.Queries.Dtos;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Repairs.Queries.GetRepair
{
    [Authorise]
    public class GetRepairQuery :IRequest<RepairVm>
    {
        public int Id { get; set; }
    }

    public class GetRepairQueryHandler : IRequestHandler<GetRepairQuery, RepairVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRepairQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RepairVm> Handle(GetRepairQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Repairs
                .Include(r => r.Vehicle)
                .Include(r => r.RepairShop)
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Repair), request.Id);

            return new RepairVm
            {
                Repair = mapper.Map<RepairDto>(entity)
            };
        }
    }
}
