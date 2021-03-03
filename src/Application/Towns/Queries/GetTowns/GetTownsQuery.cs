using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Towns.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Towns.Queries.GetTowns
{
    public class GetTownsQuery : IRequest<TownsVm>
    {
    }

    public class GetTownsQueryHandler : IRequestHandler<GetTownsQuery, TownsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTownsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TownsVm> Handle(GetTownsQuery request, CancellationToken cancellationToken) =>
            new TownsVm
            {
                Towns = await context.Towns
                    .ProjectTo<TownDto>(mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync()
            };
    }
}
