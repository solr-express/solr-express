using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Serialization;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Result
{
    public class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        private bool executed = false;

        IEnumerable<FacetKeyValue> IFacetsResult<TDocument>.Data { get; set; }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (!this.executed && currentToken == JsonToken.StartObject && currentPath.StartsWith("facet_counts"))
            {
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Converters.Add(new GeoCoordinateConverter());
                jsonSerializer.Converters.Add(new DateTimeConverter());
                jsonSerializer.ContractResolver = new CustomContractResolver();

                if (((IFacetsResult<TDocument>)this).Data == null)
                {
                    ((IFacetsResult<TDocument>)this).Data = new List<FacetKeyValue>();
                }

                while (jsonReader.Read())
                {
                    if (jsonReader.Path.StartsWith("facet_counts.facet_fields."))
                    {
                        var keyValue = new FacetKeyValue<string>()
                        {
                            Name = (string)jsonReader.Value,
                            FacetType = FacetType.Field
                        };

                        jsonReader.Read();// Start array
                        jsonReader.Read();// First element

                        while (jsonReader.TokenType != JsonToken.EndArray)
                        {
                            if (jsonReader.Path.StartsWith($"facet_counts.facet_fields.{keyValue.Name}"))
                            {
                                var value = new FacetItemValue<string>
                                {
                                    Key = (string)jsonReader.Value,
                                    Quantity = (long)jsonReader.ReadAsInt32()
                                };

                                ((List<FacetItemValue<string>>)keyValue.Data).Add(value);
                            }

                            jsonReader.Read();
                        }

                        ((List<FacetKeyValue>)((IFacetsResult<TDocument>)this).Data).Add(keyValue);
                    }
                }

                this.executed = true;
            }
        }
    }
}
