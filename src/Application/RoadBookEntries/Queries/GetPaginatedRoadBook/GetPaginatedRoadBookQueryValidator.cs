using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.RoadBookEntries.Queries.GetPaginatedRoadBook
{
    public class GetPaginatedRoadBookQueryValidator : AbstractValidator<GetPaginatedRoadBookQuery>
    {
        public GetPaginatedRoadBookQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetPaginatedRoadBookQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetPaginatedRoadBookQuery.PageSize)));
            RuleFor(q => q.From)
                .LessThan(q => q.To);
        }
    }
}
