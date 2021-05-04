using FluentValidation;

namespace CarsManager.Application.Employees.Commands.UpdateEmployedStatus
{
    public class UpdateEmployedStatusCommandValidator : AbstractValidator<UpdateEmployedStatusCommand>
    {
        public UpdateEmployedStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0);
        }
    }
}
