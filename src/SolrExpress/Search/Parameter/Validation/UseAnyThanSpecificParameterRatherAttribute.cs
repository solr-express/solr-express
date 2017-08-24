using System;
using System.Collections.Generic;

namespace SolrExpress.Search.Parameter.Validation
{
    /// <summary>
    /// Check for mistakes in use of AnyParameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class UseAnyThanSpecificParameterRatherAttribute : Attribute, IValidationAttribute
    {
        bool IValidationAttribute.IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
        {
            var specificParameters = new List<string> {
                "facet.field",
                "facet.limit",
                "facet.query",
                "facet.range",
                "fl",
                "fq",
                "mm",
                "q",
                "qf",
                "rows",
                "sort",
                "start"
            };

            errorMessage = string.Format(Resource.UseSpecificParameterRatherThanAnyException, ((IAnyParameter)searchParameter).Name);

            return !specificParameters.Contains(((IAnyParameter)searchParameter).Name.ToLowerInvariant());
        }
    }
}
