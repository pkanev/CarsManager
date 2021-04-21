using System.Collections.Generic;

namespace Client.Core.Dtos
{
    public class PaginatedListDto<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; } 
        public int TotalCount { get; set; }

        public bool HasPreviousPage => Items != null && PageIndex > 1;
        public bool HasNextPage => Items != null && PageIndex < TotalPages;
    }
}
