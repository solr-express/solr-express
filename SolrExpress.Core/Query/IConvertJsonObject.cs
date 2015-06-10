using Newtonsoft.Json.Linq;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// Base interface used to parse the SOLR results when a json object is necessary
    /// </summary>
    public interface IConvertJsonObject
    {
        /// <summary>
        /// Execute the parse of the JSON object
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        void Execute(JObject jsonObject);
    }
}
