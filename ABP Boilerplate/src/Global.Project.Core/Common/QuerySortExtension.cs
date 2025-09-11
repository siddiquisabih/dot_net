using System.Collections.Generic;
using System.Linq;

namespace Global.Project.Common
{
    public static class QuerySortExtension
    {
        public static List<T> ApplySorting<T>(this List<T> list, string sortBy, bool isDescending)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return list;

            var prop = typeof(T).GetProperty(sortBy, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            if (prop == null)
                return list;

            return isDescending
                ? list.OrderByDescending(x => prop.GetValue(x, null)).ToList()
                : list.OrderBy(x => prop.GetValue(x, null)).ToList();
        }
    }
}
