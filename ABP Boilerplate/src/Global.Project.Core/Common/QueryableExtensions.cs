using System.Linq;
using System.Threading.Tasks;
using Global.Project.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Global.Project.Common
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> query,
            PaginationInput input)
        {
            var totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(input.SortBy))
            {
                var property = typeof(T).GetProperty(input.SortBy);
                if (property != null)
                {
                    query = input.IsDescending
                        ? query.OrderByDescending(x => property.GetValue(x, null))
                        : query.OrderBy(x => property.GetValue(x, null));
                }
            }

            var items = await query
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            return new PaginatedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = input.PageNumber,
                PageSize = input.PageSize
            };
        }
    }
}