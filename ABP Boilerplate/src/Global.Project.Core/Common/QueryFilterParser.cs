using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Abp.Linq.Expressions;

namespace Global.Project.Common
{
    public static class QueryFilterParser
    {
        public static Expression<Func<T, bool>> Parse<T>(string filters)
        {
            if (string.IsNullOrEmpty(filters))
                return x => true;

            var predicate = PredicateBuilder.New<T>(true);
            var param = Expression.Parameter(typeof(T), "x");
            
            foreach (var filter in filters.Split('&'))
            {
                var match = Regex.Match(filter, @"^(\w+)(!=|>=|<=|=|>|<)(.*)$");
                var property = Expression.Property(param, match.Groups[1].Value);
                var value = ParseValue(match.Groups[3].Value.Trim(), property.Type);
                
                // Directly use the lambda expression from CreateComparison
                predicate = predicate.And(CreateComparison<T>(property, match.Groups[2].Value, value));
            }

            return predicate;
        }

        private static object ParseValue(string value, Type targetType)
        {
            if (targetType == typeof(int))
                return int.Parse(value);
            if (targetType == typeof(DateTime))
                return DateTime.Parse(value);
            if (targetType == typeof(bool))
                return bool.Parse(value);

            return value.Trim('"');
        }

        private static Expression<Func<T, bool>> CreateComparison<T>(MemberExpression property, string operatorSymbol, object value)
        {
            var param = property.Expression as ParameterExpression;
            
            // Handle string comparisons differently
            if (property.Type == typeof(string) && operatorSymbol == "=")
            {
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                // Convert both property and value to lowercase
                var lowerProperty = Expression.Call(property, toLowerMethod);
                var lowerValue = Expression.Constant(value.ToString().ToLower(), typeof(string));

                // Create contains expression
                var containsExpression = Expression.Call(lowerProperty, containsMethod, lowerValue);
                return Expression.Lambda<Func<T, bool>>(containsExpression, param);
            }

            // Existing comparison logic for other types/operators
            var convertedValue = Expression.Constant(value, property.Type);
            Expression body = operatorSymbol switch
            {
                "=" => Expression.Equal(property, convertedValue),
                "!=" => Expression.NotEqual(property, convertedValue),
                ">" => Expression.GreaterThan(property, convertedValue),
                ">=" => Expression.GreaterThanOrEqual(property, convertedValue),
                "<" => Expression.LessThan(property, convertedValue),
                "<=" => Expression.LessThanOrEqual(property, convertedValue),
                _ => throw new ArgumentException($"Invalid operator: {operatorSymbol}")
            };

            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
