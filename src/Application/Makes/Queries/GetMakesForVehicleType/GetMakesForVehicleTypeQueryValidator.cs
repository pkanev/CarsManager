using FluentValidation;

namespace CarsManager.Application.Makes.Queries.GetMakesForVehicleType
{
    public class GetMakesForVehicleTypeQueryValidator : AbstractValidator<GetMakesForVehicleTypeQuery>
    {
        public GetMakesForVehicleTypeQueryValidator()
        {
            RuleFor(q => q.VehicleType)
                .IsInEnum();
        }
    }
}
