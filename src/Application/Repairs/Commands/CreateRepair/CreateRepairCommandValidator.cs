using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Repairs.Commands.CreateRepair
{
    public class CreateRepairCommandValidator : AbstractValidator<CreateRepairCommand>
    {
        public CreateRepairCommandValidator()
        {
            RuleFor(r => r.Mileage)
                .GreaterThanOrEqualTo(RepairConstants.MIN_MILEAGE);
            RuleFor(r => r.InitialPrice)
                .GreaterThanOrEqualTo(RepairConstants.MIN_INITIAL_PRICE);
        }
    }
}
