using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Repairs.Commands.UpdateRepair
{
    public class UpdateRepairCommandValidator : AbstractValidator<UpdateRepairCommand>
    {
        public UpdateRepairCommandValidator()
        {
            RuleFor(r => r.Mileage)
                .GreaterThanOrEqualTo(RepairConstants.MIN_MILEAGE);
            RuleFor(r => r.FinalPrice)
                .GreaterThanOrEqualTo(RepairConstants.MIN_INITIAL_PRICE);
        }
    }
}
