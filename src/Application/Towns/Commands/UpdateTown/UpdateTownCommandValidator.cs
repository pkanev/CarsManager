using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Towns.Commands.UpdateTown
{
    public class UpdateTownCommandValidator : AbstractValidator<UpdateTownCommand>
    {
        public UpdateTownCommandValidator()
        {
            RuleFor(t => t.Name)
                .MaximumLength(TownConstants.NAME_MAX_LENGTH)
                .NotEmpty();
        }
    }
}
