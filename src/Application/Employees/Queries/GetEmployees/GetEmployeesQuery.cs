using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Employees.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<EmployeesVm>
    {
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
            return new EmployeesVm
            {
                Employees = await context.Employees
                    .ProjectTo<EmployeeDto>(mapper.ConfigurationProvider)
                    .OrderBy(e => e.Surname)
                    .ThenBy(e => e.GivenName)
                    .ToListAsync()
            };
        }
    }
}
