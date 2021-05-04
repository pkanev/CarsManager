using FluentValidation;

namespace CarsManager.Application.Vehicles.Commands.UpdateBlockedStatus
{
    public class UpdateBlockedStatusCommandValidator : AbstractValidator<UpdateBlockedStatusCommand>
    {
        public UpdateBlockedStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
