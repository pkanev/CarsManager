using CarsManager.Application.Common.Constants;

namespace CarsManager.Application.Repairs.Queries.GetRepairsForVehicleWithPagination
{
    public class GetRepairsForVehicleWithPaginationQueryDto
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }
}
