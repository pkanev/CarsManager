using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Repairs.Queries.GetRepairsForVehicleWithPagination
{
    public class GetRepairsForVehicleWithPaginationQueryValidator : AbstractValidator<GetRepairsForVehicleWithPaginationQuery>
    {
        public GetRepairsForVehicleWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetRepairsForVehicleWithPaginationQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetRepairsForVehicleWithPaginationQuery.PageSize)));
        }
    }
}
