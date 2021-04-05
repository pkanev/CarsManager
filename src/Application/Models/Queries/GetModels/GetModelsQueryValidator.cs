using FluentValidation;

namespace CarsManager.Application.Models.Queries.GetModels
{
    public class GetModelsQueryValidator : AbstractValidator<GetModelsQuery>
    {
        public GetModelsQueryValidator()
        {
            RuleFor(q => q.VehicleType)
                .IsInEnum();
        }
    }
}
