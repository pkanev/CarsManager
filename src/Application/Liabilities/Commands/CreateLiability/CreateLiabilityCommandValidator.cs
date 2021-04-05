using FluentValidation;

namespace CarsManager.Application.Liabilities.Commands.CreateLiability
{
    public class CreateLiabilityCommandValidator : AbstractValidator<CreateLiabilityCommand>
    {
        public CreateLiabilityCommandValidator()
        {
            RuleFor(c => c.EndDate)
                .GreaterThanOrEqualTo(c => c.StartDate);
            RuleFor(c => c.Liability)
                .IsInEnum();
        }
    }
}
