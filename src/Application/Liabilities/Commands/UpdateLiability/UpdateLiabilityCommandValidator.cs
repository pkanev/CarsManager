using FluentValidation;

namespace CarsManager.Application.Liabilities.Commands.UpdateLiability
{
    public class UpdateLiabilityCommandValidator : AbstractValidator<UpdateLiabilityCommand>
    {
        public UpdateLiabilityCommandValidator()
        {
            RuleFor(c => c.EndDate)
                .GreaterThanOrEqualTo(c => c.StartDate);
            RuleFor(c => c.Liability)
                .IsInEnum();
        }
    }
}
