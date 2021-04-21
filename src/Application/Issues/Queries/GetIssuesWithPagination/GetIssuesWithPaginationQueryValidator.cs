using FluentValidation;

namespace CarsManager.Application.Issues.Queries.GetIssuesWithPagination
{
    public class GetIssuesWithPaginationQueryValidator : AbstractValidator<GetIssuesWithPaginationQuery>
    {
        public GetIssuesWithPaginationQueryValidator()
        {
            RuleFor(i => i.IssueType)
                .IsInEnum();
        }
    }
}
