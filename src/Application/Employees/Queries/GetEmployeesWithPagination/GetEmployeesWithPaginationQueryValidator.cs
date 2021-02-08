using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Employees.Queries.GetEmployeesWithPagination
{
    public class GetEmployeesWithPaginationQueryValidator : AbstractValidator<GetEmployeesWithPaginationQuery>
    {
        public GetEmployeesWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetEmployeesWithPaginationQuery.PageNumber)));

            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetEmployeesWithPaginationQuery.PageSize)));
        }
    }
}
