using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Users.Queries.GetUsersWithPagination
{
    public class GetUsersWithPaginationQueryValidator : AbstractValidator<GetUsersWithPaginationQuery>
    {
        public GetUsersWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetUsersWithPaginationQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetUsersWithPaginationQuery.PageSize)));
        }
    }
}
