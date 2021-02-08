using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.MOTs.Queries.GetMOT
{
    public class GetMOTQuery : IRequest<MOTVm>
    {
        public int Id { get; set; }
    }

    public class GetMOTQueryHandler : IRequestHandler<GetMOTQuery, MOTVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMOTQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MOTVm> Handle(GetMOTQuery request, CancellationToken cancellationToken)
        {
            var entity = await context
                .MOTs
                .Include(m => m.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(MOT), request.Id);

            return new MOTVm { MOT = mapper.Map<MOTDto>(entity) };
        }
    }
}
