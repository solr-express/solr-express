using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.Linq;
using SolrExpress.Core.Entity;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Facet field data builder
    /// </summary>
    public sealed class FacetFieldResultBuilder : IResultBuilder, IConvertJsonObject
    {
        /// <summary>
        /// Execute the JSON object parse in facet field list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facet_counts"] == null || jsonObject["facet_counts"]["facet_fields"] == null)
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

            this.Data = new List<FacetKeyValue<string>>();

            var list = jsonObject["facet_counts"]["facet_fields"]
                .Children()
                .ToList();

            var facets = list
                .Select(item =>
                {
                    var value = new FacetKeyValue<string>()
                    {
                        Name = ((JProperty)item).Name,
                        Data = new Dictionary<string, long>()
                    };

                    var array = ((JArray)((JProperty)(item)).Value);

                    for (int i = 0; i < array.Count; i += 2)
                    {
                        value.Data[array[i].ToObject<string>()] = array[i + 1].ToObject<long>();
                    }

                    return value;
                });

            this.Data.AddRange(facets);
        }

        public List<FacetKeyValue<string>> Data { get; set; }
    }
}
