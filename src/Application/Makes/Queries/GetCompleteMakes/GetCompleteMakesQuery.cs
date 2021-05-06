using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Makes.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Makes.Queries.GetCompleteMakes
{
    [Authorise]
    public class GetCompleteMakesQuery : IRequest<IList<MakeAndModelDto>>
    {
    }

    public class GetCompleteMakesQueryHandler : IRequestHandler<GetCompleteMakesQuery, IList<MakeAndModelDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCompleteMakesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<MakeAndModelDto>> Handle(GetCompleteMakesQuery request, CancellationToken cancellationToken)
        {
            var result = await context.Makes
                .Include(m => m.Models)
                .ProjectTo<MakeAndModelDto>(mapper.ConfigurationProvider)
                .OrderBy(m => m.Name)
                .ToListAsync();
            return result;
        }
    }
}
