using FluentValidation;

namespace CarsManager.Application.Liabilities.Commands.UpdateLiability
{
    public class UpdateLiabilityCommandValidator : AbstractValidator<UpdateLiabilityCommand>
    {
        public UpdateLiabilityCommandValidator()
        {
            RuleFor(c => c.DurationDays)
                .GreaterThan(0);
            RuleFor(c => c.Liability)
                .IsInEnum();
        }
    }
}
