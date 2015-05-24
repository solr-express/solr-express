using Newtonsoft.Json.Linq;
using SolrExpress.QueryBuilder;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet query data builder
    /// </summary>
    public class FacetQueryResultBuilder : IResultBuilder
    {
        /// <summary>
        /// Execute the parse of the JSON object in facet query list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facets"] != null)
            {
                var list = jsonObject["facets"].Children().Where(q =>
                    q is JProperty &&
                    q.Children().Count() == 1 &&
                    ((JProperty)q).Value is JObject &&
                    ((JObject)((JProperty)q).Value)["count"] != null);

                if (list.Any())
                {
                    this.Data = list.ToDictionary(
                        k => ((JProperty)k).Name,
                        v => ((JObject)((JProperty)v).Value)["count"].ToObject<long>());
                }
                else
                {
                    this.Data = new Dictionary<string, long>();
                }

                return;
            }

            throw new Exception.UnexpectedJsonFormatException(jsonObject.ToString());
        }

        public Dictionary<string, long> Data { get; set; }
    }
}
