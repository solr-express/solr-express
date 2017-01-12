using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    /// <summary>
    /// Internal use
    /// </summary>
    internal class SystemParameter<TDocument> : BaseSystemParameter<TDocument>, ISearchParameterExecute<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            foreach (var parameter in this.Parameters)
            {
                if (jObj[parameter.Key] == null)
                {
                    jObj.Add(new JProperty(parameter.Key, parameter.Value));
                }
            }

            jObject["params"] = jObj;

            if (jObject["query"] == null)
            {
                jObject["query"] = new JValue("*:*");
            }

            if (jObject["sort"] == null)
            {
                jObject["sort"] = new JValue("score desc");
            }

            if (jObject["fields"] == null)
            {
                var jArray = new JArray();
                jArray.Add("*,score");

                jObject["fields"] = jArray;
            }
        }
    }
}
