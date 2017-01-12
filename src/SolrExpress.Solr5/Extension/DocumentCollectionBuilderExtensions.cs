using SolrExpress.Core;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Update;
using SolrExpress.Solr5.Search;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Solr5.Search.Result;

namespace SolrExpress.Solr5.Extension
{
    public static class DocumentCollectionBuilderExtensions
    {
        public static DocumentCollectionBuilder<TDocument> UseSolr5<TDocument>(this DocumentCollectionBuilder<TDocument> builder)
            where TDocument : IDocument
        {
            builder
                .Engine
#if NETCORE
                .AddSingleton<IEngine, NetCoreEngine>((NetCoreEngine)builder.Engine)
#else
                .AddSingleton<IEngine, NetFrameworkEngine>((NetFrameworkEngine)builder.Engine)
#endif
                .AddSingleton<ISolrConnection, SolrConnection>()
                .AddTransient<IAnyParameter<TDocument>, AnyParameter<TDocument>>()
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
                .AddTransient<IRandomSortParameter<TDocument>, RandomSortParameter<TDocument>>()
                .AddTransient<ISpatialFilterParameter<TDocument>, SpatialFilterParameter<TDocument>>()
                .AddTransient<IBoostParameter<TDocument>, BoostParameter<TDocument>>()
                .AddTransient<IDocumentResult<TDocument>, DocumentResult<TDocument>>()
                .AddTransient<IFacetFieldResult<TDocument>, FacetFieldResult<TDocument>>()
                .AddTransient<IFacetQueryResult<TDocument>, FacetQueryResult<TDocument>>()
                .AddTransient<IFacetRangeResult<TDocument>, FacetRangeResult<TDocument>>()
                .AddTransient<IInformationResult<TDocument>, InformationResult<TDocument>>()
                .AddTransient<IAtomicUpdate<TDocument>, AtomicUpdate<TDocument>>()
                .AddTransient<IAtomicDelete<TDocument>, AtomicDelete<TDocument>>()
                .AddTransient<ISearchParameterCollection<TDocument>, SearchParameterCollection<TDocument>>()
                .AddTransient<ISystemParameter<TDocument>, SystemParameter<TDocument>>();

            return builder;
        }
    }
}
