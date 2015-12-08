using Newtonsoft.Json.Linq;
using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Builder
{
    /// <summary>
    /// Facet query data builder
    /// </summary>
    public sealed class FacetQueryResultBuilder<TDocument> : IFacetQueryResultBuilder<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parse of the facet query list in the JSON object
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facets"] != null)
            {
                var list = jsonObject["facets"]
                    .Children()
                    .Where(q =>
                        q is JProperty &&
                        q.Values().Count() == 1 &&
                        ((JProperty)q).Value is JObject &&
                        ((JObject)((JProperty)q).Value)["count"] != null)
                    .ToList();

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

            throw new UnexpectedJsonFormatException(jsonObject.ToString());
        }

        /// <summary>
        /// Facet data
        /// </summary>
        public Dictionary<string, long> Data { get; set; }
    }
}
