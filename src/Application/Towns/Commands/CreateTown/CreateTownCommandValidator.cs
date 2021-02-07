using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Towns.Commands.CreateTown
{
    public class CreateTownCommandValidator : AbstractValidator<CreateTownCommand>
    {
        public CreateTownCommandValidator()
        {
            RuleFor(t => t.Name)
                .MaximumLength(TownConstants.NAME_MAX_LENGTH)
                .NotEmpty();
        }
    }
}
