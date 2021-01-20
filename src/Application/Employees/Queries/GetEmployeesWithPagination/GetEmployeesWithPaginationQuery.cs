using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using MediatR;

namespace CarsManager.Application.Employees.Queries.GetEmployeesWithPagination
{
    public class GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetEmployeesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await context.Employees
                .OrderBy(e => e.Surname)
                .ThenBy(e => e.GivenName)
                .ProjectTo<EmployeeDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
