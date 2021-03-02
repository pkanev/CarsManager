using FluentValidation;

namespace CarsManager.Application.Liabilities.Commands.DeleteLiability
{
    public class DeleteLiabilityCommandValidator : AbstractValidator<DeleteLiabilityCommand>
    {
        public DeleteLiabilityCommandValidator()
        {
            RuleFor(c => c.Liability)
                .IsInEnum();
        }
    }
}
