using System;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Check for mistakes in use of RangeParameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FacetRangeTypeAttribute : Attribute, IValidationAttribute
    {
        bool IValidationAttribute.IsValid(ISearchParameter searchParameter, out string errorMessage)
        {
            // TODO: Need review in IFacetRangeParameter<TDocument>

            //var propertyType = this._expressionBuilder.GetPropertyTypeFromExpression(this.Expression);

            //switch (propertyType.ToString())
            //{
            //    case "System.Int32":
            //    case "System.Int64":
            //    case "System.Single":
            //    case "System.Double":
            //    case "System.Decimal":
            //    case "System.DateTime":
            //        break;
            //    default:
            //        isValid = false;

            //        errorMessage = Resource.FieldMustBeNumericOrDateTimeToBeUsedInFacetRangeException;
            //        break;
            //}

            throw new NotImplementedException();
        }
    }
}
