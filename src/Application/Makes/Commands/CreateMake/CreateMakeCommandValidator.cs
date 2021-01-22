using FluentValidation;

namespace CarsManager.Application.Makes.Commands.CreateMake
{
    public class CreateMakeCommandValidator : AbstractValidator<CreateMakeCommand>
    {
        public CreateMakeCommandValidator()
        {
            RuleFor(m => m.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
