using System.Linq;
using CarsManager.Application.Common.Extensions;
using CarsManager.Application.Common.Interfaces;
using FluentValidation;

namespace CarsManager.Application.Makes.Commands.UpdateMake
{
    public class UpdateMakeCommandValidator : AbstractValidator<UpdateMakeCommand>
    {
        public UpdateMakeCommandValidator(IApplicationDbContext context)
        {
            var allMakes = context.Makes.Select(m => new UpdateMakeCommand { Id= m.Id, Name = m.Name });

            RuleFor(m => m.Name)
                .MaximumLength(50)
                .IsUnique(allMakes, isCaseSensitive: false, needsTrimming: true).WithMessage("There already exists a car make with the same name.")
                .NotEmpty();
        }
    }
}
