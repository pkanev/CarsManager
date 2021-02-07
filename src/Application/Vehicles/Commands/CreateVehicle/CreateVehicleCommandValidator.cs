using System;
using System.Text.RegularExpressions;
using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
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
                .Matches(VehicleConstants.LICENSE_PLATE_REGEX, RegexOptions.IgnoreCase)
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
            RuleFor(v => v.MOT.DurationDays)
                .GreaterThanOrEqualTo(0)
                .When(v => v.MOT != null);
            RuleFor(v => v.CivilLiability.DurationDays)
                .GreaterThanOrEqualTo(0)
                .When(v => v.CivilLiability != null);
            RuleFor(v => v.CarInsurance.DurationDays)
                .GreaterThanOrEqualTo(0)
                .When(v => v.CarInsurance != null);
            RuleFor(v => v.Vignette.DurationDays)
                .GreaterThanOrEqualTo(0)
                .When(v => v.Vignette != null);
        }
    }
}
