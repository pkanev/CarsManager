using FluentValidation;

namespace CarsManager.Application.Towns.Commands.CreateTown
{
    public class CreateTownCommandValidator : AbstractValidator<CreateTownCommand>
    {
        public CreateTownCommandValidator()
        {
            RuleFor(t => t.Name)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
