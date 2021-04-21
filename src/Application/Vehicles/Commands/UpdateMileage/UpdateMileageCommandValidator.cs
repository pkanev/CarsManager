using FluentValidation;

namespace CarsManager.Application.Vehicles.Commands.UpdateMileage
{
    public class UpdateMileageCommandValidator : AbstractValidator<UpdateMileageCommand>
    {
        public UpdateMileageCommandValidator()
        {
            RuleFor(v => v.Mileage)
                .GreaterThanOrEqualTo(0);
        }
    }
}
