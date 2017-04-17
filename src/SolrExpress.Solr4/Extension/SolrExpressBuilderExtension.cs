using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using SolrExpress.Solr4.Search;
using SolrExpress.Solr4.Search.Parameter;
using SolrExpress.Solr4.Search.Result;

namespace SolrExpress.Solr4.Extension
{
    public static class SolrExpressBuilderExtension
    {
        /// <summary>
        /// Use SolrExpress services implemented in SOLR 4
        /// </summary>
        /// <param name="solrExpressBuilder">Builder to control SolrExpress behavior</param>
        /// <returns>Builder to control SolrExpress behavior</returns>
        public static SolrExpressBuilder<TDocument> UseSolr5<TDocument>(this SolrExpressBuilder<TDocument> solrExpressBuilder)
            where TDocument : IDocument
        {
            solrExpressBuilder
                .ServiceProvider
                .AddTransient<IAnyParameter, AnyParameter>()
                .AddTransient<IBoostParameter<TDocument>, BoostParameter<TDocument>>()
                .AddTransient<IFacetFieldParameter<TDocument>, FacetFieldParameter<TDocument>>()
                .AddTransient<IFacetLimitParameter<TDocument>, FacetLimitParameter<TDocument>>()
                .AddTransient<IFacetQueryParameter<TDocument>, FacetQueryParameter<TDocument>>()
                .AddTransient<IFacetRangeParameter<TDocument>, FacetRangeParameter<TDocument>>()
                .AddTransient<IFacetSpatialParameter<TDocument>, FacetSpatialParameter<TDocument>>()
                .AddTransient<IFieldsParameter<TDocument>, FieldsParameter<TDocument>>()
                .AddTransient<IFilterParameter<TDocument>, FilterParameter<TDocument>>()
                .AddTransient<ILimitParameter<TDocument>, LimitParameter<TDocument>>()
                .AddTransient<IMinimumShouldMatchParameter<TDocument>, MinimumShouldMatchParameter<TDocument>>()
                .AddTransient<IOffsetParameter<TDocument>, OffsetParameter<TDocument>>()
                .AddTransient<IQueryFieldParameter<TDocument>, QueryFieldParameter<TDocument>>()
                .AddTransient<IQueryParameter<TDocument>, QueryParameter<TDocument>>()
                .AddTransient<ISortParameter<TDocument>, SortParameter<TDocument>>()
                .AddTransient<ISortRandomlyParameter<TDocument>, SortRandomlyParameter<TDocument>>()
                .AddTransient<ISpatialFilterParameter<TDocument>, SpatialFilterParameter<TDocument>>()
                .AddTransient<IFacetsResult<TDocument>, FacetsResult<TDocument>>()
                .AddTransient<ISearchItemCollection<TDocument>, SearchItemCollection<TDocument>>();

            return solrExpressBuilder;
        }
    }
}
