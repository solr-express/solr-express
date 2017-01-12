using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public sealed class QueryFieldParameter<TDocument> : BaseQueryFieldParameter<TDocument>, ISearchParameterExecute<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();
            var jProperty = new JProperty("qf", this.Expression);

            jObj.Add(jProperty);

            jObject["params"] = jObj;
        }
    }
}
