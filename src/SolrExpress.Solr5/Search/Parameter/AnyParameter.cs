using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class AnyParameter : BaseAnyParameter, ISearchParameter<JObject>
    {
        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            jObj.Add(new JProperty(this.Name, this.Value));

            jObject["params"] = jObj;
        }
    }
}
