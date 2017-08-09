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
        private ProcessFacetFields<TDocument> _processFacetFields = new ProcessFacetFields<TDocument>();
        private ProcessFacetQueries<TDocument> _processFacetQueries = new ProcessFacetQueries<TDocument>();
        private ProcessFacetRanges<TDocument> _processFacetRanges = new ProcessFacetRanges<TDocument>();

        IEnumerable<IFacetItem> IFacetsResult<TDocument>.Data { get; set; }

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
                    ((IFacetsResult<TDocument>)this).Data = new List<IFacetItem>();
                }

                var facetItems = ((List<IFacetItem>)((IFacetsResult<TDocument>)this).Data);

                if (jsonReader.Path.StartsWith("facet_counts.facet_fields."))
                {
                    facetItems.Add(this._processFacetFields.Execute(jsonReader));
                }
                if (jsonReader.Path.StartsWith("facet_counts.facet_queries"))
                {
                    facetItems.AddRange(this._processFacetQueries.Execute(jsonReader));
                }
                if (jsonReader.Path.StartsWith("facet_counts.facet_ranges."))
                {
                    facetItems.Add(this._processFacetRanges.Execute(searchParameters, jsonReader));
                }
            }
        }
    }
}
