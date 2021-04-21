using FluentValidation;

namespace CarsManager.Application.RoadBookEntries.Queries.GetRoadBook
{
    public class GetRoadBookQueryValidator : AbstractValidator<GetRoadBookQuery>
    {
        public GetRoadBookQueryValidator()
        {
            RuleFor(q => q.From)
                .LessThan(q => q.To);
        }
    }
}
