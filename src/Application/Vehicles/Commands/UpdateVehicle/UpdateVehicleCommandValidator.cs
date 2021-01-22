using FluentValidation;

namespace CarsManager.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(v => v.Model)
                .NotNull();
            RuleFor(v => v.EngineDisplacement)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.Mileage)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.Color)
                .NotEmpty();
            RuleFor(v => v.BeltMileage)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.BrakeLiningsMileage)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.BrakeDisksMileage)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.CoolantMileage)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.FuelConsumption)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.OilMileage)
                .GreaterThanOrEqualTo(0);
        }
    }
}
