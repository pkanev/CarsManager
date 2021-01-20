using FluentValidation;

namespace CarsManager.Application.Employees.Queries.GetEmployeesWithPagination
{
    public class GetEmployeesWithPaginationQueryValidator : AbstractValidator<GetEmployeesWithPaginationQuery>
    {
        private const string MESSAGE = "{0} at least greater than or equal to 1.";

        public GetEmployeesWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage(string.Format(MESSAGE, nameof(GetEmployeesWithPaginationQuery.PageNumber)));

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage(string.Format(MESSAGE, nameof(GetEmployeesWithPaginationQuery.PageSize)));
        }
    }
}
