using System;
using System.Linq;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Valid if associete field of parameter is Indexed=true
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldMustBeStoredTrueAttribute : Attribute, IValidationAttribute
    {
        public bool IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
            where TDocument : Document
        {
            var searchParameterFieldExpressions = searchParameter as ISearchItemFieldExpressions<TDocument>;
            var searchParameterFieldExpression = searchParameter as ISearchItemFieldExpression<TDocument>;

            var fieldExpressions =
                searchParameterFieldExpressions?.FieldExpressions ??
                new[] { searchParameterFieldExpression?.FieldExpression };

            var expressionBuilder =
                searchParameterFieldExpressions?.ExpressionBuilder ??
                searchParameterFieldExpression?.ExpressionBuilder;

            if (fieldExpressions.Any(fieldExpression => !expressionBuilder?.GetIsStored(fieldExpression) ?? false))
            {
                errorMessage = Resource.FieldMustBeStoredTrueToBeUsedInThisFunctionException;
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
