using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SolrExpress.Utility
{
    // <summary>
    // Helper class used to process expressions
    // </summary>
    public static class ExpressionUtil
    {
        private static Expression GetRootExpression(Expression expression)
        {
            var rootExpression = expression;

            while (rootExpression is MemberExpression memberExpression)
            {
                rootExpression = memberExpression.Expression;
            }

            return rootExpression;
        }

        /// <summary>
        /// Get property referenced into indicated expression 
        /// </summary>
        /// <param name="expression">Expression used to find property info</param>
        /// <returns>Property referenced into indicated expression</returns>
        internal static PropertyInfo GetPropertyInfoFromExpression(Expression expression)
        {
            PropertyInfo propertyInfo = null;
            var lambda = (LambdaExpression)ExpressionUtil.GetRootExpression(expression);

            MemberExpression memberExpression;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    var unaryExpression = (UnaryExpression)lambda.Body;

                    memberExpression = (MemberExpression)unaryExpression.Operand;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    Checker.IsNull(propertyInfo, Resource.ExpressionMustBePropertyException);

                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)lambda.Body;

                    propertyInfo = memberExpression.Member as PropertyInfo;

                    Checker.IsNull(propertyInfo, Resource.ExpressionMustBePropertyException);

                    break;
                default:
                    throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
            }

            if (propertyInfo == null)
            {
                throw new InvalidOperationException(Resource.UnknownToResolveExpressionException);
            }

            return propertyInfo;
        }
    }
}
