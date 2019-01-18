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
        public bool IsValid<TDocument>(ISearchParameter searchParameter, out string errorMessage)
            where TDocument : Document
        {
            var anyParameter = searchParameter as IAnyParameter;
            
            if (anyParameter == null)
            {
                errorMessage = null;
                return true;
            }

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

            errorMessage = string.Format(Resource.UseSpecificParameterRatherThanAnyException, anyParameter.Name);

            return !specificParameters.Contains(anyParameter.Name.ToLowerInvariant());
        }
    }
}
