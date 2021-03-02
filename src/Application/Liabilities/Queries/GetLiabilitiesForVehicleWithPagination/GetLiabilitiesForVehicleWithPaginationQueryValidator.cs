using CarsManager.Application.Common.Constants;
using FluentValidation;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicleWithPagination
{
    public class GetLiabilitiesForVehicleWithPaginationQueryValidator : AbstractValidator<GetLiabilitiesForVehicleWithPaginationQuery>
    {
        public GetLiabilitiesForVehicleWithPaginationQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetLiabilitiesForVehicleWithPaginationQuery.PageNumber)));
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage(string.Format(PageConstants.MESSAGE, nameof(GetLiabilitiesForVehicleWithPaginationQuery.PageSize)));
            RuleFor(q => q.Liability)
                .IsInEnum();
        }
    }
}
