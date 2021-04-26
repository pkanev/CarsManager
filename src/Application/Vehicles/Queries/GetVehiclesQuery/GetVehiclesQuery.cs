using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Vehicles.Queries.GetVehicle;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesQuery
{
    [Authorise]
    public class GetVehiclesQuery : IRequest<VehiclesVm>
    {
        public string Path { get; set; }
    }

    public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, VehiclesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUrlHelper urlHelper;

        public GetVehiclesQueryHandler(IApplicationDbContext context, IMapper mapper, IUrlHelper urlHelper)
        {
            this.context = context;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
        }

        public async Task<VehiclesVm> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await context.Vehicles
                .Include(v => v.Model)
                .Include(v => v.Model.Make)
                .Include(v => v.RoadBookEntries)
                .OrderBy(v => v.LicencePlate)
                .ProjectTo<VehicleDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            foreach (var vehicle in vehicles)
            if (!string.IsNullOrEmpty(vehicle.ImageName))
                vehicle.ImageAddress = urlHelper.UrlCombine(request.Path, vehicle.ImageName);

            return new VehiclesVm
            {
                Vehicles = vehicles,
            };
        }
    }
}
