using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class FacetLimitParameter : BaseFacetLimitParameter, ISearchParameter<JObject>
    {
        /// <summary>
        /// Execute the creation of the parameter "rows"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();
            var jProperty = new JProperty("facet.limit", this.Value);

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }
    }
}
