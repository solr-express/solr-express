using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;

namespace SolrExpress.Solr5.Search.Parameter
{
    public class RandomSortParameter<TDocument> : BaseRandomSortParameter<TDocument>, ISearchParameterExecute<JObject>
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var fieldName = "random";

            var jValue = (JValue)jObject["sort"] ?? new JValue((string)null);

            var value = $"{fieldName} {(this.Ascendent ? "asc" : "desc")}";

            if (jValue.Value != null)
            {
                jValue.Value += $", {value}";
            }
            else
            {
                jValue.Value = value;
            }

            jObject["sort"] = jValue;
        }
    }
}
