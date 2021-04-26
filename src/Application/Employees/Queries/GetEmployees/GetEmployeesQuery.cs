using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Queries.GetEmployees
{
    [Authorise]
    public class GetEmployeesQuery : IRequest<EmployeesVm>
    {
        public string PhotoPath { get; set; }
    }

    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, EmployeesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeesVm> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await context.Employees
                    .ProjectTo<BasicEmployeeDto>(mapper.ConfigurationProvider)
                    .OrderBy(e => e.Surname)
                    .ThenBy(e => e.GivenName)
                    .ThenBy(e => e.MiddleName)
                    .ToListAsync();

            foreach (var employee in employees)
                if (!string.IsNullOrEmpty(employee.ImageName))
                    employee.ImageAddress = Path.Combine(request.PhotoPath, employee.ImageName);

            return new EmployeesVm { Employees = employees };
        }
    }
}
