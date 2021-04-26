using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Models.Queries.Dtos;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Models.Queries.GetModel
{
    [Authorise]
    public class GetModelQuery : IRequest<ModelVm>
    {
        public int Id { get; set; }
    }

    public class GetModelQueryHandler : IRequestHandler<GetModelQuery, ModelVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetModelQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ModelVm> Handle(GetModelQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Models
                .Include(m => m.Make)
                .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Model), request.Id);

            var vehicles = await context.Vehicles
                    .AsNoTracking()
                    .ProjectTo<VehicleForModelDto>(mapper.ConfigurationProvider)
                    .OrderBy(v => v.LicencePlate)
                    .ToListAsync(cancellationToken);

            return new ModelVm
            {
                Model = mapper.Map<ModelDto>(entity),
                Vehicles = vehicles,
            };
        }
    }
}
