using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using CarsManager.Application.Liabilities.Queries.GetLiability;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicle
{
    [Authorise]
    public class GetLiabilitiesForVehicleQuery : IRequest<LiabilitiesVm>
    {
        public int VehicleId { get; set; }
        public LiabilityType Liability { get; set; }
    }

    public class GetLiabilitiesQueryHandler : IRequestHandler<GetLiabilitiesForVehicleQuery, LiabilitiesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetLiabilitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LiabilitiesVm> Handle(
            GetLiabilitiesForVehicleQuery request,
            CancellationToken cancellationToken)
            => request.Liability switch
            {
                LiabilityType.MOT => new LiabilitiesVm { Liabilities = await GetMOTs(request.VehicleId) },
                LiabilityType.CivilLiability => new LiabilitiesVm { Liabilities = await GetCivilLiabilities(request.VehicleId) },
                LiabilityType.CarInsurance => new LiabilitiesVm { Liabilities = await GetCarInsurances(request.VehicleId) },
                LiabilityType.Vignette => new LiabilitiesVm { Liabilities = await GetVignettes(request.VehicleId) },
                _ => throw new InvalidLiabilityTypeException($"Invalid liability type: {request.Liability}")
            };

        private async Task<List<GetLiabilityDto>> GetMOTs(int vehicleId)
        {
            var liabilities = await context.MOTs
                .Where(l => l.VehicleId == vehicleId)
                .OrderByDescending(l => l.StartDate)
                .ProjectTo<GetLiabilityDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return AssignLiabilityType(liabilities, LiabilityType.MOT);
        }

        private async Task<List<GetLiabilityDto>> GetCivilLiabilities(int vehicleId)
        {
            var liabilities = await context.CivilLiabilities
                .Where(l => l.VehicleId == vehicleId)
                .OrderByDescending(l => l.StartDate)
                .ProjectTo<GetLiabilityDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return AssignLiabilityType(liabilities, LiabilityType.CivilLiability);
        }

        private async Task<List<GetLiabilityDto>> GetCarInsurances(int vehicleId)
        {
            var liabilities = await context.CarInsurances
                .Where(l => l.VehicleId == vehicleId)
                .OrderByDescending(l => l.StartDate)
                .ProjectTo<GetLiabilityDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return AssignLiabilityType(liabilities, LiabilityType.CarInsurance);
        }

        private async Task<List<GetLiabilityDto>> GetVignettes(int vehicleId)
        {
            var liabilities = await context.Vignettes
            .Where(l => l.VehicleId == vehicleId)
            .OrderByDescending(l => l.StartDate)
            .ProjectTo<GetLiabilityDto>(mapper.ConfigurationProvider)
            .ToListAsync();

            return AssignLiabilityType(liabilities, LiabilityType.Vignette);
        }

        private List<GetLiabilityDto> AssignLiabilityType(List<GetLiabilityDto> list, LiabilityType liabilityType)
            => list
                .Select(x => { x.LiabilityType = (int)liabilityType; return x; })
                .ToList();
        
    }
}
