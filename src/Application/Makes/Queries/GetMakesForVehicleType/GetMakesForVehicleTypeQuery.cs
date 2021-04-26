using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Makes.Queries.Dtos;
using CarsManager.Application.Makes.Queries.GetMakes;
using CarsManager.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Makes.Queries.GetMakesForVehicleType
{
    [Authorise]
    public class GetMakesForVehicleTypeQuery : IRequest<MakesVm>
    {
        public VehicleType VehicleType { get; set; }
    }

    public class GetMakesForVehicleTypeQueryHandler : IRequestHandler<GetMakesForVehicleTypeQuery, MakesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMakesForVehicleTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MakesVm> Handle(
            GetMakesForVehicleTypeQuery request,
            CancellationToken cancellationToken) => new MakesVm
            {
                Makes = await context.Makes
                    .Include(m => m.Models)
                    .Where(m => m.Models.Any(m => m.VehicleType == request.VehicleType))
                    .ProjectTo<MakeDto>(mapper.ConfigurationProvider)
                    .OrderBy(m => m.Name)
                    .ToListAsync()
            };
    }
}
