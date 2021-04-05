using System;
using System.Linq;
using System.Text.RegularExpressions;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Extensions;
using CarsManager.Application.Common.Interfaces;
using FluentValidation;

namespace CarsManager.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator(IApplicationDbContext context)
        {
            var allVehicles = context.Vehicles.Select(v => new CreateVehicleCommand { LicencePlate = v.LicencePlate });

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
                .NotEmpty()
                .IsUnique(allVehicles, isCaseSensitive: false, needsTrimming: true).WithMessage("Има кола с такъв регистрационен номер.");
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
            RuleFor(v => v.MOT.EndDate)
                .GreaterThanOrEqualTo(v => v.MOT.StartDate)
                .When(v => v.MOT != null);
            RuleFor(v => v.CivilLiability.EndDate)
                .GreaterThanOrEqualTo(v => v.CivilLiability.StartDate)
                .When(v => v.CivilLiability != null);
            RuleFor(v => v.CarInsurance.EndDate)
                .GreaterThanOrEqualTo(v => v.CarInsurance.StartDate)
                .When(v => v.CarInsurance != null);
            RuleFor(v => v.Vignette.EndDate)
                .GreaterThanOrEqualTo(v => v.Vignette.StartDate)
                .When(v => v.Vignette != null);
        }
    }
}
