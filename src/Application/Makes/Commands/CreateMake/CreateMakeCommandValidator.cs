using System.Linq;
using CarsManager.Application.Common.Extensions;
using CarsManager.Application.Common.Interfaces;
using FluentValidation;

namespace CarsManager.Application.Makes.Commands.CreateMake
{
    public class CreateMakeCommandValidator : AbstractValidator<CreateMakeCommand>
    {
        public CreateMakeCommandValidator(IApplicationDbContext context)
        {
            var allMakes = context.Makes.Select(m => new CreateMakeCommand { Name = m.Name });

            RuleFor(m => m.Name)
                .MaximumLength(50)
                .IsUnique(allMakes, isCaseSensitive: false, needsTrimming: true).WithMessage("There already exists a car make with the same name.")
                .NotEmpty();
        }
    }
}
