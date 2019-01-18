using System;
using System.Linq;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Check for mistakes in use of RangeParameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FacetRangeTypeAttribute : Attribute, IValidationAttribute
    {
        public bool IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
            where TDocument : Document
        {
            errorMessage = string.Empty;

            var facetRangeParameter = (IFacetRangeParameter<TDocument>)searchParameter;

            var propertyType = facetRangeParameter.ExpressionBuilder.GetPropertyType(facetRangeParameter.FieldExpression);

            var allowedTypes = new[]
            {
                typeof(DateTime),
                typeof(DateTime?),
                typeof(decimal),
                typeof(decimal?),
                typeof(double),
                typeof(double?),
                typeof(float),
                typeof(float?),
                typeof(int),
                typeof(int?),
                typeof(long),
                typeof(long?),
                typeof(short),
                typeof(short?),
            };

            if (!allowedTypes.Contains(propertyType))
            {
                errorMessage = Resource.FieldMustBeNumericOrDateTimeToBeUsedInFacetRangeException;
                return false;
            }

            return true;
        }
    }
}
