using System;
using System.Collections.Generic;
using System.Linq;

namespace Global.Project.Common
{
    public static class QueryFilterExtensions
    {
        public static List<T> ApplyQueryFilter<T>(this List<T> list, string filters)
        {
            if (string.IsNullOrEmpty(filters))
                return list;

            try
            {
                var expression = QueryFilterParser.Parse<T>(filters);
                if (expression == null)
                    return list;
                try
                {
                    var filterExpression = expression.Compile();
                    return list.Where(x => {
                        if (x == null) return false;

                        try
                        {
                            var result = filterExpression(x);
                            return result;
                        }
                        catch (NullReferenceException)
                        {
                            return false;
                        }
                    }).ToList();
                }
                catch (Exception)
                {
                    return list;
                }
            }
            catch (Exception)
            {
                // If parsing fails, return the original list
                return list;
            }
        }
    }
}