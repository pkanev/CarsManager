using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Liabilities.Queries.GetLiability
{
    public class GetLiabilityQuery : IRequest<LiabilityVm>
    {
        public int Id { get; set; }
        public LiabilityType Liability { get; set; }
    }

    public class GetLiabilityQueryHandler : IRequestHandler<GetLiabilityQuery, LiabilityVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILiabilityUtils liabilityUtils;

        public GetLiabilityQueryHandler(IApplicationDbContext context, IMapper mapper, ILiabilityUtils liabilityUtils)
        {
            this.context = context;
            this.mapper = mapper;
            this.liabilityUtils = liabilityUtils;
        }

        public async Task<LiabilityVm> Handle(GetLiabilityQuery request, CancellationToken cancellationToken)
        {
            var entity = await GetLiabilityWithVehilcleInfo(request.Id, request.Liability);

            if (entity == null)
                throw new NotFoundException(liabilityUtils.GetLiabilityName(request.Liability), request.Id);

            var liability = mapper.Map<GetLiabilityDto>(entity);
            liability.LiabilityType = (int)request.Liability;
            return new LiabilityVm { Liability = liability };
        }

        private async Task<Liability> GetLiabilityWithVehilcleInfo(int id, LiabilityType liability) => liability switch
        {
            LiabilityType.MOT => await context
                .MOTs
                .Include(m => m.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id),

            LiabilityType.CivilLiability => await context
                .CivilLiabilities
                .Include(m => m.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id),

            LiabilityType.CarInsurance => await context
                .CarInsurances
                .Include(m => m.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id),

            LiabilityType.Vignette => await context
                .Vignettes
                .Include(m => m.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id),
            _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {liability}")
        };
    }
}
