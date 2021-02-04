using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQuery : IRequest<VehicleVm>
    {
        public int Id { get; set; }
    }

    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, VehicleVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetVehicleQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<VehicleVm> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles
                .Include(v => v.Model)
                .Include(v => v.Model.Make)
                .FirstOrDefaultAsync(v => v.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            return new VehicleVm
            {
                Vehicle = mapper.Map<VehicleDto>(entity)
            };
        }
    }
}
