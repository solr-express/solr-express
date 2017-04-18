using System;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Check for mistakes in use of RangeParameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FacetRangeTypeAttribute : Attribute, IValidationAttribute
    {
        bool IValidationAttribute.IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
        {
            errorMessage = string.Empty;

            var facetRangeParameter = (IFacetRangeParameter<TDocument>)searchParameter;

            var propertyType = facetRangeParameter.ExpressionBuilder.GetPropertyType(facetRangeParameter.FieldExpression);

            switch (propertyType.ToString())
            {
                case "System.Int32":
                case "System.Int64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                case "System.DateTime":
                    return true;
                default:
                    errorMessage = Resource.FieldMustBeNumericOrDateTimeToBeUsedInFacetRangeException;
                    return false;
            }
        }
    }
}
