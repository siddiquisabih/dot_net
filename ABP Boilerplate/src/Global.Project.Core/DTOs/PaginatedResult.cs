using System;
using System.Collections.Generic;

namespace Global.Project.DTOs
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalRecords { get; set; }

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }

}