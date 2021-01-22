using FluentValidation;

namespace CarsManager.Application.Makes.Commands.UpdateMake
{
    public class UpdateMakeCommandValidator : AbstractValidator<UpdateMakeCommand>
    {
        public UpdateMakeCommandValidator()
        {
            RuleFor(m => m.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
