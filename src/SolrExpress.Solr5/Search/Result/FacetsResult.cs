using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Serialization;
using System.Collections.Generic;

namespace SolrExpress.Solr5.Search.Result
{
    public class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        IEnumerable<IFacetItem> IFacetsResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (currentPath.StartsWith("facets."))
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Converters.Add(new GeoCoordinateConverter());
                jsonSerializer.Converters.Add(new DateTimeConverter());
                jsonSerializer.ContractResolver = new CustomContractResolver();

                if (((IFacetsResult<TDocument>)this).Data == null)
                {
                    ((IFacetsResult<TDocument>)this).Data = new List<IFacetItem>();
                }

                var facetItems = ((List<IFacetItem>)((IFacetsResult<TDocument>)this).Data);

                var facetName = (string)jsonReader.Value;
                IFacetItem facetItem = null;

                jsonReader.Read();// Starts object
                jsonReader.Read();// Go to checker element

                // Facet query
                if ((string)jsonReader.Value == "count")
                {
                    facetItem = new FacetItemQuery(facetName, (long)jsonReader.ReadAsInt32());
                }
                else // Facet field or facet range
                {
                }

                // Closes facet item object
                while (!(jsonReader.Path.Equals($"facets.{facetName}") && jsonReader.TokenType == JsonToken.EndObject))
                {
                    jsonReader.Read();
                }

                facetItems.Add(facetItem);
            }
        }
    }
}
