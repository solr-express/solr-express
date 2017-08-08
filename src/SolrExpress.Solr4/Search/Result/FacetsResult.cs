using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Serialization;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Result
{
    public class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        IEnumerable<FacetItem> IFacetsResult<TDocument>.Data { get; set; }

        private void ExecuteFacetFields(JsonReader jsonReader)
        {
            var facetItem = new FacetItemMultiValues<string>()
            {
                Name = (string)jsonReader.Value,
                FacetType = FacetType.Field
            };

            jsonReader.Read();// Start array
            jsonReader.Read();// First element

            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                if (jsonReader.Path.StartsWith($"facet_counts.facet_fields.{facetItem.Name}"))
                {
                    var value = new FacetValue<string>
                    {
                        Key = (string)jsonReader.Value,
                        Quantity = (long)jsonReader.ReadAsInt32()
                    };

                    ((List<FacetValue<string>>)facetItem.Data).Add(value);
                }

                jsonReader.Read();
            }

            ((List<FacetItem>)((IFacetsResult<TDocument>)this).Data).Add(facetItem);
        }

        private void ExecuteFacetQueries(JsonReader jsonReader)
        {
            jsonReader.Read();// Start object
            jsonReader.Read();// Start property name

            while (jsonReader.TokenType != JsonToken.EndObject)
            {
                var facetItem = new FacetItemSingleValue
                {
                    Name = (string)jsonReader.Value,
                    Quantity = (long)jsonReader.ReadAsInt32(),
                    FacetType = FacetType.Query
                };

                ((List<FacetItem>)((IFacetsResult<TDocument>)this).Data).Add(facetItem);

                jsonReader.Read();
            }
        }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (currentPath.StartsWith("facet_counts."))
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Converters.Add(new GeoCoordinateConverter());
                jsonSerializer.Converters.Add(new DateTimeConverter());
                jsonSerializer.ContractResolver = new CustomContractResolver();

                if (((IFacetsResult<TDocument>)this).Data == null)
                {
                    ((IFacetsResult<TDocument>)this).Data = new List<FacetItem>();
                }

                if (jsonReader.Path.StartsWith("facet_counts.facet_fields."))
                {
                    this.ExecuteFacetFields(jsonReader);
                }
                if (jsonReader.Path.StartsWith("facet_counts.facet_queries"))
                {
                    this.ExecuteFacetQueries(jsonReader);
                }
            }
        }
    }
}
