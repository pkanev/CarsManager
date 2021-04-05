using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Employees.Queries.GetEmployees;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Vehicles.Queries.GetVehicleExtended
{
    public class GetVehicleExtendedQuery : IRequest<VehicleExtendedVm>
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
    }

    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleExtendedQuery, VehicleExtendedVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IUrlHelper urlHelper;

        public GetVehicleQueryHandler(IApplicationDbContext context, IMapper mapper, IUrlHelper urlHelper)
        {
            this.context = context;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
        }

        public async Task<VehicleExtendedVm> Handle(GetVehicleExtendedQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Vehicles
                .Include(v => v.Employees)
                .Include(v => v.MOTs)
                .Include(v => v.CivilLiabilities)
                .Include(v => v.CarInsurances)
                .Include(v => v.Vignettes)
                .FirstOrDefaultAsync(v => v.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Vehicle), request.Id);

            var employees = entity.Employees
                .OrderBy(e => e.Surname)
                .ThenBy(e => e.GivenName)
                .Select(e => mapper.Map<BasicEmployeeDto>(e)).ToList();

            foreach (var employee in employees)
                if (!string.IsNullOrEmpty(employee.ImageName))
                    employee.ImageAddress = Path.Combine(request.PhotoPath, employee.ImageName);

            return new VehicleExtendedVm
            {
                RecentLiabilities = mapper.Map<VehicleRecentLiabilitiesDto>(entity),
                Employees = employees,
            };
        }
    }
}
