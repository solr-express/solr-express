using Newtonsoft.Json.Linq;
using SolrExpress.QueryBuilder;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet range data builder
    /// </summary>
    public class FacetRangeResultBuilder : IResultBuilder
    {
        /// <summary>
        /// Execute the parse of the JSON object in facet range list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            throw new Exception.UnexpectedJsonFormatException(jsonObject.ToString());
        }
    }
}
