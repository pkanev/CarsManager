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
        public string Path { get; set; }
    }

    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, VehicleVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUrlHelper urlHelper;

        public GetVehicleQueryHandler(IApplicationDbContext context, IMapper mapper, IUrlHelper urlHelper)
        {
            this.context = context;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
        }

        public async Task<VehicleVm> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles
                .Include(v => v.Model)
                .Include(v => v.Model.Make)
                .FirstOrDefaultAsync(v => v.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            var vehicle = mapper.Map<VehicleDto>(entity);

            if (!string.IsNullOrEmpty(vehicle.ImageName))
                vehicle.ImageAddress = urlHelper.UrlCombine(request.Path, vehicle.ImageName);
            
            return new VehicleVm
            {
                Vehicle = vehicle,
            };
        }
    }
}
