using Newtonsoft.Json;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Search.Result
{
    public class FacetsResult<TDocument> : IFacetsResult<TDocument>
        where TDocument : Document
    {
        IEnumerable<IFacetItem> IFacetsResult<TDocument>.Data { get; set; }

        private ISearchParameter GetSearchParameter(List<ISearchParameter> searchParameters, string facetName)
        {
            return searchParameters
                 .Where(parameter => parameter is IFacetRangeParameter<TDocument> || parameter is IFacetFieldParameter<TDocument>)
                 .FirstOrDefault(parameter =>
                 {
                     var searchItemFieldExpression = ((ISearchItemFieldExpression<TDocument>)parameter);

                     var fieldName = searchItemFieldExpression
                         .ExpressionBuilder
                         .GetFieldName(searchItemFieldExpression.FieldExpression);

                     var facetRangeParameter = parameter as IFacetRangeParameter<TDocument>;

                     return
                         facetName.Equals(facetRangeParameter?.AliasName) ||
                         facetName.Equals(fieldName);
                 });
        }

        private IFacetItem GetFacetItem(List<ISearchParameter> searchParameters, JsonReader jsonReader)
        {
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
                var searchParameter = this.GetSearchParameter(searchParameters, facetName);
                var isFacetRange = (searchParameter as IFacetRangeParameter<TDocument>) != null;

                jsonReader.Read();// Starts array

                if (isFacetRange)
                {
                    //facetItem = new FacetItemRange(facetName);
                }
                else
                {
                    facetItem = new FacetItemField(facetName);

                    jsonReader.Read();// Starts array

                    while (jsonReader.Path.StartsWith($"facets.{facetName}.buckets["))
                    {
                        var starterPath = jsonReader.Path;

                        jsonReader.Read();// "val" property
                        var key = jsonReader.ReadAsString();

                        jsonReader.Read();// "count" property
                        var count = jsonReader.ReadAsInt32();

                        var value = new FacetItemFieldValue
                        {
                            Key = key,
                            Quantity = (long)count
                        };

                        ((List<FacetItemFieldValue>)((FacetItemField)facetItem).Values).Add(value);

                        // Closes bucket object
                        while (!jsonReader.Path.Equals(starterPath))
                        {
                            jsonReader.Read();
                        }

                        jsonReader.Read();// Starts next bucket object
                    }
                }
            }

            // Closes facet item object
            while (!(jsonReader.Path.Equals($"facets.{facetName}") && jsonReader.TokenType == JsonToken.EndObject))
            {
                jsonReader.Read();
            }

            return facetItem;
        }

        void ISearchResult.Execute(List<ISearchParameter> searchParameters, JsonToken currentToken, string currentPath, JsonReader jsonReader)
        {
            if (currentPath.StartsWith("facets."))
            {
                if (((IFacetsResult<TDocument>)this).Data == null)
                {
                    ((IFacetsResult<TDocument>)this).Data = new List<IFacetItem>();
                }

                var facetItems = ((List<IFacetItem>)((IFacetsResult<TDocument>)this).Data);
                IFacetItem facetItem = this.GetFacetItem(searchParameters, jsonReader);

                facetItems.Add(facetItem);
            }
        }
    }
}
