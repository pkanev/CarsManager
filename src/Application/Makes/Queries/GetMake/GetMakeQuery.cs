using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Makes.Queries.Dtos;
using CarsManager.Application.Models.Queries.Dtos;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Makes.Queries.GetMake
{
    public class GetMakeQuery : IRequest<MakeVm>
    {
        public int Id { get; set; }
    }

    public class GetMakeQueryHandler : IRequestHandler<GetMakeQuery, MakeVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMakeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MakeVm> Handle(GetMakeQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Makes
                .Include(m => m.Models)
                .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Make), request.Id);

            return new MakeVm
            {
                Make = mapper.Map<MakeDto>(entity),
                Models = entity.Models.Select(m => mapper.Map<ModelDto>(m)).ToList()
            };
        }
    }
}
