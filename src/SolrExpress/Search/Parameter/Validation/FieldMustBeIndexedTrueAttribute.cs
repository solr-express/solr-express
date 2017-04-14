using System;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Valid if associete field of parameter is Indexed=true
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldMustBeIndexedTrueAttribute : Attribute, IValidationAttribute
    {
        bool IValidationAttribute.IsValid(ISearchParameter searchParameter, out string errorMessage)
        {
            // TODO: Need review in ISearchParameterFieldExpressions and ISearchParameterFieldExpression interfaces

            //var searchParameterFieldExpressions = searchParameter as ISearchParameterFieldExpressions<TDocument>;
            //var searchParameterFieldExpression = searchParameter as ISearchParameterFieldExpression<TDocument>;

            //var solrFieldAttribute = searchParameter.ExpressionBuilder.GetSolrFieldAttributeFromPropertyInfo(this.Expression);

            //if (solrFieldAttribute?.Indexed ?? true)
            //{
            //    return;
            //}

            errorMessage = string.Empty;
            return true;
        }
    }
}
