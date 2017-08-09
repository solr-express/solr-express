using Newtonsoft.Json;
using SolrExpress.Search.Result;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Result
{
    /// <summary>
    ///  Process parsing of facets of type field
    /// </summary>
    internal class ProcessFacetFields<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Execute processing
        /// </summary>
        internal FacetItemField Execute(JsonReader jsonReader)
        {
            var facetName = (string)jsonReader.Value;
            var facetItem = new FacetItemField(facetName);

            jsonReader.Read();// Start array
            jsonReader.Read();// First element

            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.Path.StartsWith($"facet_counts.facet_fields.{facetName}"))
                {
                    var value = new FacetItemFieldValue
                    {
                        Key = (string)jsonReader.Value,
                        Quantity = (long)jsonReader.ReadAsInt32()
                    };

                    ((List<FacetItemFieldValue>)facetItem.Values).Add(value);
                }

                jsonReader.Read();
            }

            return facetItem;
        }
    }
}
