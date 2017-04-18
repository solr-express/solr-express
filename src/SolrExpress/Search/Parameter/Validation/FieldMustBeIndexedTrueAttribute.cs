using System;
using System.Linq.Expressions;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Valid if associete field of parameter is Indexed=true
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldMustBeIndexedTrueAttribute : Attribute, IValidationAttribute
    {
        bool IValidationAttribute.IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
        {
            var searchParameterFieldExpressions = searchParameter as ISearchItemFieldExpressions<TDocument>;
            var searchParameterFieldExpression = searchParameter as ISearchItemFieldExpression<TDocument>;

            var fieldExpressions =
                searchParameterFieldExpressions?.FieldExpressions ??
                new Expression<Func<TDocument, object>>[] { searchParameterFieldExpression.FieldExpression };

            var expressionBuilder =
                searchParameterFieldExpressions?.ExpressionBuilder ??
                searchParameterFieldExpression.ExpressionBuilder;

            foreach (var fieldExpression in fieldExpressions)
            {
                if (!expressionBuilder.GetIsIndexed(fieldExpression))
                {
                    errorMessage = Resource.FieldMustBeIndexedTrueToBeUsedInThisFunctionException;
                    return false;
                }
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
