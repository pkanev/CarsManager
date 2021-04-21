using CarsManager.Application.Common.Constants;

namespace CarsManager.Application.Common.Dtos
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = PageConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PageConstants.DEFAULT_PAGE_SIZE;
    }
}
