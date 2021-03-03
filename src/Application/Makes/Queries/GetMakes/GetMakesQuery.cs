using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Makes.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Makes.Queries.GetMakes
{
    public class GetMakesQuery : IRequest<MakesVm>
    {
    }

    public class GetMakesQueryHandler : IRequestHandler<GetMakesQuery, MakesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMakesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MakesVm> Handle(GetMakesQuery request, CancellationToken cancellationToken) => 
            new MakesVm
            {
                Makes = await context.Makes
                    .ProjectTo<MakeDto>(mapper.ConfigurationProvider)
                    .OrderBy(m => m.Name)
                    .ToListAsync()
            };
    }
}
