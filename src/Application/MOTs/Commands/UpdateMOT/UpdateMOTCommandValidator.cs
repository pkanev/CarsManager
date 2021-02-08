using FluentValidation;

namespace CarsManager.Application.MOTs.Commands.UpdateMOT
{
    public class UpdateMOTCommandValidator : AbstractValidator<UpdateMOTCommand>
    {
        public UpdateMOTCommandValidator()
        {
            RuleFor(c => c.DurationDays)
                .GreaterThan(0);
        }
    }
}
