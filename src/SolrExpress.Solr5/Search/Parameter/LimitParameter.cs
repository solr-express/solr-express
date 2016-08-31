using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class LimitParameter : BaseLimitParameter, ISearchParameter<JObject>
    {
        /// <summary>
        /// Execute the creation of the parameter "limit"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            jObject["limit"] = new JValue(this.Value);
        }
    }
}
