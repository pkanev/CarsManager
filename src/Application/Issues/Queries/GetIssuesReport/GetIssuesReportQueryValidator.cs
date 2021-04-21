using FluentValidation;

namespace CarsManager.Application.Issues.Queries.GetIssuesReport
{
    public class GetIssuesReportQueryValidator : AbstractValidator<GetIssuesReportQuery>
    {
        public GetIssuesReportQueryValidator()
        {
            RuleFor(i => i.IssueType)
                .IsInEnum();
        }
    }
}
