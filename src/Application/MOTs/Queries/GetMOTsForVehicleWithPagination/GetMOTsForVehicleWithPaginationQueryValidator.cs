using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.MOTs.Queries.GetMOTsForVehicleWithPagination
{
    public class GetMOTsForVehicleWithPaginationQueryValidator : AbstractValidator<GetMOTsForVehicleWithPaginationQuery>
    {
        public GetMOTsForVehicleWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetMOTsForVehicleWithPaginationQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetMOTsForVehicleWithPaginationQuery.PageSize)));
        }
    }
}
