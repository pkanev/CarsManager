using System.Linq;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Extensions;
using CarsManager.Application.Common.Interfaces;
using FluentValidation;

namespace CarsManager.Application.RepairShops.Commands.UpdateRepairShop
{
    public class UpdateRepairShopCommandValidator : AbstractValidator<UpdateRepairShopCommand>
    {
        public UpdateRepairShopCommandValidator(IApplicationDbContext context)
        {
            var allShops = context.RepairShops.Select(s => new UpdateRepairShopCommand { Id = s.Id, Name = s.Name });

            RuleFor(s => s.Name)
                .MaximumLength(RepairShopConstants.NAME_MAX_LENGTH)
                .IsUnique(allShops, isCaseSensitive: false, needsTrimming: true).WithMessage($"There already exists a repair shop with the same name.")
                .NotEmpty();
        }
    }
}
