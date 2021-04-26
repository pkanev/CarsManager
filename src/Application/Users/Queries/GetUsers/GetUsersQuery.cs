using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Application.Users.Queries.GetUsers
{
    [Authorise(Roles = RoleConstants.ADMIN)]
    public class GetUsersQuery : IRequest<IList<UserDto>>
    {
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IList<UserDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            => await context.Users
            .Include(u => u.Roles)
            .OrderBy(u => u.Username)
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
