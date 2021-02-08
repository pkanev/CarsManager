using FluentValidation;

namespace CarsManager.Application.MOTs.Commands.CreateMOT
{
    public class CreateMOTCommandValidator : AbstractValidator<CreateMOTCommand>
    {
        public CreateMOTCommandValidator()
        {
            RuleFor(c => c.DurationDays)
                .GreaterThan(0);
        }
    }
}
