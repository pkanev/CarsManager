using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Application.Common.Models;
using MediatR;

namespace CarsManager.Application.Employees.Queries.GetEmployeesWithPagination
{
    public class GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
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

        public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken) =>
            await context.Employees
                .OrderBy(e => e.Surname)
                .ThenBy(e => e.GivenName)
                .ProjectTo<EmployeeDto>(mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
