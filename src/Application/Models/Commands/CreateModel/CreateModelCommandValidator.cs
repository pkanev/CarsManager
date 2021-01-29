using System.Linq;
using CarsManager.Application.Common.Interfaces;
using FluentValidation;

namespace CarsManager.Application.Models.Commands.CreateModel
{
    public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
    {
        private readonly IApplicationDbContext context;

        public CreateModelCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(b => b)
                .Must(ValidateModelIsUnique)
                .WithMessage("There already exists a model with the same name.");

            RuleFor(m => m.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(m => m.VehicleType).IsInEnum();

        }

        private bool ValidateModelIsUnique(CreateModelCommand command)
        {
            var currentModels = context.Models
                .Where(m => m.MakeId == command.MakeId);

            return !currentModels.Any(m => m.Name.ToLower() == command.Name.Trim().ToLower());
        }
    }
}
