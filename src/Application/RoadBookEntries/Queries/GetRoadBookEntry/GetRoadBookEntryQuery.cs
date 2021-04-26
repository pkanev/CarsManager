using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.RoadBookEntries.Queries.GetRoadBookEntry
{
    [Authorise]
    public class GetRoadBookEntryQuery : IRequest<RoadBookBasicEntryDto>
    {
        public int Id { get; set; }
    }

    public class GetRoadBookEntryQueryHandler : IRequestHandler<GetRoadBookEntryQuery, RoadBookBasicEntryDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRoadBookEntryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RoadBookBasicEntryDto> Handle(GetRoadBookEntryQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.RoadBookEntries.FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(RoadBookEntry), request.Id);

            return mapper.Map<RoadBookBasicEntryDto>(entity);
        }
    }
}
