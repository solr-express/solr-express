using Newtonsoft.Json.Linq;

namespace SolrExpress.QueryBuilder
{
    /// <summary>
    /// Base interface used to parse the SOLR results
    /// </summary>
    public interface IResultBuilder
    {
        /// <summary>
        /// Execute the parse of the JSON object
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void Execute(JObject jsonObject);
    }
}
