using FluentValidation;

namespace CarsManager.Application.Liabilities.Commands.CreateLiability
{
    public class CreateLiabilityCommandValidator : AbstractValidator<CreateLiabilityCommand>
    {
        public CreateLiabilityCommandValidator()
        {
            RuleFor(c => c.DurationDays)
                .GreaterThan(0);
            RuleFor(c => c.Liability)
                .IsInEnum();
        }
    }
}
