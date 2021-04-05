using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQuery : IRequest<EmployeeVm>
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeeVm> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var entity = await context
                .Employees
                .Include(e => e.Town)
                .Include(e => e.Vehicles)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Make)
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            var employee = mapper.Map<EmployeeDto>(entity);
            if (!string.IsNullOrEmpty(employee.ImageName))
                employee.ImageAddress = Path.Combine(request.PhotoPath, employee.ImageName);

            return new EmployeeVm
            {
                Employee = employee,
                Vehicles = entity.Vehicles.Select(v => mapper.Map<VehicleForEmployeeDto>(v)).ToList()
            };
        }
    }
}
