using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Vehicles.Queries.GetVehiclesWithPagination
{
    public class GetVehiclesWithPaginationQueryValidator : AbstractValidator<GetVehiclesWithPaginationQuery>
    {
        public GetVehiclesWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetVehiclesWithPaginationQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetVehiclesWithPaginationQuery.PageSize)));
        }
    }
}
