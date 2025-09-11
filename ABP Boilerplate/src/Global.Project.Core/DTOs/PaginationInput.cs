using Global.Project.Binders;
using Microsoft.AspNetCore.Mvc;

namespace Global.Project.DTOs
{
    public class PaginationInput
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        [ModelBinder(BinderType = typeof(QueryFilterModelBinder))]
        public string Filters { get; set; }

        public string SortBy { get; set; }

        public bool IsDescending { get; set; }

        public string SearchTerm { get; set; }
    }
}