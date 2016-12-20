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
                .AddSingleton<DocumentCollectionOptions<TDocument>, DocumentCollectionOptions<TDocument>>(builder.Options)
                .AddSingleton<ISearchParameterBuilder<TDocument>, SearchParameterBuilder<TDocument>>()
                .AddTransient<ISolrSearch<TDocument>, SolrSearch<TDocument>>()
                .AddTransient<ISolrAtomicUpdate<TDocument>, SolrAtomicUpdate<TDocument>>()
                .AddSingleton<ISolrConnection, SolrConnection>()
                .AddTransient<IAnyParameter, AnyParameter>()
                .AddTransient<IFacetFieldParameter<TDocument>, FacetFieldParameter<TDocument>>()
                .AddTransient<IFacetLimitParameter, FacetLimitParameter>()
                .AddTransient<IFacetQueryParameter<TDocument>, FacetQueryParameter<TDocument>>()
                .AddTransient<IFacetRangeParameter<TDocument>, FacetRangeParameter<TDocument>>()
                .AddTransient<IFacetSpatialParameter<TDocument>, FacetSpatialParameter<TDocument>>()
                .AddTransient<IFieldsParameter<TDocument>, FieldsParameter<TDocument>>()
                .AddTransient<IFilterParameter<TDocument>, FilterParameter<TDocument>>()
                .AddTransient<ILimitParameter, LimitParameter>()
                .AddTransient<IMinimumShouldMatchParameter, MinimumShouldMatchParameter>()
                .AddTransient<IOffsetParameter, OffsetParameter>()
                .AddTransient<IQueryFieldParameter, QueryFieldParameter>()
                .AddTransient<IQueryParameter<TDocument>, QueryParameter<TDocument>>()
                .AddTransient<ISortParameter<TDocument>, SortParameter<TDocument>>()
                .AddTransient<IRandomSortParameter, RandomSortParameter>()
                .AddTransient<ISpatialFilterParameter<TDocument>, SpatialFilterParameter<TDocument>>()
                .AddTransient<IBoostParameter<TDocument>, BoostParameter<TDocument>>()
                .AddTransient<IDocumentResult<TDocument>, DocumentResult<TDocument>>()
                .AddTransient<IFacetFieldResult<TDocument>, FacetFieldResult<TDocument>>()
                .AddTransient<IFacetQueryResult<TDocument>, FacetQueryResult<TDocument>>()
                .AddTransient<IFacetRangeResult<TDocument>, FacetRangeResult<TDocument>>()
                .AddTransient<IInformationResult<TDocument>, InformationResult<TDocument>>()
                .AddTransient<IAtomicUpdate<TDocument>, AtomicUpdate<TDocument>>()
                .AddTransient<IAtomicDelete<TDocument>, AtomicDelete<TDocument>>()
                .AddTransient<ISearchParameterCollection, SearchParameterCollection>()
                .AddTransient<ISystemParameter, SystemParameter>();

            return builder;
        }
    }
}
