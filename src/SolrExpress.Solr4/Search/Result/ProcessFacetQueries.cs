using Newtonsoft.Json;
using SolrExpress.Search.Result;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Result
{
    /// <summary>
    ///  Process parsing of facets of type query
    /// </summary>
    internal class ProcessFacetQueries<TDocument>
        where TDocument : Document
    {
        /// <summary>
        /// Execute processing
        /// </summary>
        internal FacetItemQuery[] Execute(JsonReader jsonReader)
        {
            var facetItemQueries = new List<FacetItemQuery>();

            jsonReader.Read();// Start object
            jsonReader.Read();// Start property name

            while (jsonReader.TokenType != JsonToken.EndObject)
            {
                var facetItem = new FacetItemQuery(
                    (string)jsonReader.Value,
                    (long)jsonReader.ReadAsInt32());

                facetItemQueries.Add(facetItem);

                jsonReader.Read();
            }

            return facetItemQueries.ToArray();
        }
    }
}
