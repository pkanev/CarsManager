using CarsManager.Application.Common.Constants;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilitiesForVehicleWithPagination
{
    public class GetLiabilitiesForVehicleWithPaginationQueryDto
    {
        public int VehicleId { get; set; }
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }
}
