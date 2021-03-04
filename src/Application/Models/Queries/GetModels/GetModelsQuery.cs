using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Models.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Models.Queries.GetModels
{
    public class GetModelsQuery : IRequest<ModelsVm>
    {
        public int Id { get; set; }
    }

    public class GetModelsQueryHandler : IRequestHandler<GetModelsQuery, ModelsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetModelsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ModelsVm> Handle(
            GetModelsQuery request,
            CancellationToken cancellationToken) => new ModelsVm
            {
                Models = await context.Models
                    .Where(m => m.MakeId == request.Id)
                    .ProjectTo<ModelDto>(mapper.ConfigurationProvider)
                    .OrderBy(m => m.Name)
                    .ToListAsync()
            };
    }
}
