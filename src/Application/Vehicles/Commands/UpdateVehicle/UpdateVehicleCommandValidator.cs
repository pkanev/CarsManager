using System;
using System.Text.RegularExpressions;
using FluentValidation;

namespace CarsManager.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(v => v.ModelId)
                .GreaterThan(0);
            RuleFor(v => v.Year)
                .GreaterThanOrEqualTo(1900);
            RuleFor(v => v.Fuel)
                .IsInEnum().WithMessage("Invalid fuel type");
            RuleFor(v => v.EngineDisplacement)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.Mileage)
                .GreaterThanOrEqualTo(0);
            RuleFor(v => v.LicencePlate)
                .Matches(@"^[A-Z]{1,2}[0-9]{4}[A-Z]{1,2}$", RegexOptions.IgnoreCase)
                .NotEmpty();
            RuleFor(v => v.Color)
                .NotEmpty();
            RuleFor(v => v.FirstRegistration)
                .GreaterThanOrEqualTo(new DateTime(1900, 1, 1));
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
