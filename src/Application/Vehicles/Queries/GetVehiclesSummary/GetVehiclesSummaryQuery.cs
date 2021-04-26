using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesSummary
{
    [Authorise]
    public class GetVehiclesSummaryQuery : IRequest<VehiclesSummaryDto>
    {
    }

    public class GetVehiclesSummaryQueryHandler : IRequestHandler<GetVehiclesSummaryQuery, VehiclesSummaryDto>
    {
        private readonly IApplicationDbContext context;

        public GetVehiclesSummaryQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<VehiclesSummaryDto> Handle(GetVehiclesSummaryQuery request, CancellationToken cancellationToken)
            => Task.FromResult(new VehiclesSummaryDto
            {
                InUse = context.Vehicles.Where(v => v.Employees.Count() > 0).Count(),
                Total = context.Vehicles.Count()
            });
    }
}
