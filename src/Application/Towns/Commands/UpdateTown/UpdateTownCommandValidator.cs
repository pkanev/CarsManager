using FluentValidation;

namespace CarsManager.Application.Towns.Commands.UpdateTown
{
    public class UpdateTownCommandValidator : AbstractValidator<UpdateTownCommand>
    {
        public UpdateTownCommandValidator()
        {
            RuleFor(t => t.Name)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
