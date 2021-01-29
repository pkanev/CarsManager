using System.Linq;
using CarsManager.Application.Common.Interfaces;
using FluentValidation;

namespace CarsManager.Application.Models.Commands.UpdateModel
{
    public class UpdateModelCommandValidator : AbstractValidator<UpdateModelCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateModelCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(b => b)
                .Must(ValidateModelIsUnique)
                .WithMessage("There already exists a model with the same name.");

            RuleFor(m => m.Name)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(m => m.VehicleType).IsInEnum().WithMessage("Invalid vehicle type");
        }

        private bool ValidateModelIsUnique(UpdateModelCommand command)
        {
            var currentModels = context.Models
                .Where(m => m.Id != command.Id && m.MakeId == command.MakeId);

            return !currentModels.Any(m => m.Name.ToLower() == command.Name.Trim().ToLower());
        }
    }
}
