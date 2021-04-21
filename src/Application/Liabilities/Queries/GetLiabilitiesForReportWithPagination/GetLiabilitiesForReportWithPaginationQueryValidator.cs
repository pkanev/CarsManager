using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForReportWithPagination
{
    public class GetLiabilitiesForReportWithPaginationQueryValidator : AbstractValidator<GetLiabilitiesForReportWithPaginationQuery>
    {
        public GetLiabilitiesForReportWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetLiabilitiesForReportWithPaginationQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetLiabilitiesForReportWithPaginationQuery.PageSize)));
            RuleFor(q => q.Liability)
                .IsInEnum();
        }
    }
}
