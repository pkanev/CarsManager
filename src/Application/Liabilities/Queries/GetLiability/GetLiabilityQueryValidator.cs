using FluentValidation;

namespace CarsManager.Application.Liabilities.Queries.GetLiability
{
    public class GetLiabilityQueryValidator : AbstractValidator<GetLiabilityQuery>
    {
        public GetLiabilityQueryValidator()
        {
            RuleFor(q => q.Liability)
                .IsInEnum();
        }
    }
}
