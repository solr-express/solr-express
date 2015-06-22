using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Helper
{
    /// <summary>
    /// Helper class use to process arithmetic operations
    /// </summary>
    internal static class GenericHelper
    {
        /// <summary>
        /// Evaluates binary subtract (-) for the given type
        /// </summary>
        internal static T Addition<T>(T value1, T value2)
        {
            ParameterExpression lhs = Expression.Parameter(typeof(T), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof(T), "rhs");

            var exp = Expression.Lambda<Func<T, T, T>>(Expression.Add(lhs, rhs), lhs, rhs).Compile();

            return exp(value1, value2);
        }

        /// <summary>
        /// Evaluates binary subtract (-) for the given type
        /// </summary>
        internal static T Subtract<T>(T value1, T value2)
        {
            ParameterExpression lhs = Expression.Parameter(typeof(T), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof(T), "rhs");

            var exp = Expression.Lambda<Func<T, T, T>>(Expression.Subtract(lhs, rhs), lhs, rhs).Compile();

            return exp(value1, value2);
        }
    }
}
