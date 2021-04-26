using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Towns.Queries.Dtos;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Towns.Queries.GetTown
{
    [Authorise]
    public class GetTownQuery : IRequest<TownDto>
    {
        public int Id { get; set; }
    }

    public class GetTownQueryHandler : IRequestHandler<GetTownQuery, TownDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTownQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TownDto> Handle(GetTownQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Towns.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Town), request.Id);

            return mapper.Map<TownDto>(entity);
        }
    }
}
