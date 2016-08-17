using SolrExpress.Core;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.Result;
using SolrExpress.Core.Update;
using SolrExpress.Solr4.Query;
using SolrExpress.Solr4.Query.Parameter;
using SolrExpress.Solr4.Query.Result;
using SolrExpress.Solr4.Update;

namespace SolrExpress.Solr4.Extension
{
    public static class DocumentCollectionBuilderExtensions
    {
        public static DocumentCollectionBuilder<TDocument> UseSolr4<TDocument>(this DocumentCollectionBuilder<TDocument> builder)
            where TDocument : IDocument
        {
            ApplicationServices
                .Current
                .AddSingleton<ISolrConnection, SolrConnection>()
                .AddTransient<IAnyParameter, AnyParameter>()
                .AddTransient<IFacetFieldParameter<TDocument>, FacetFieldParameter<TDocument>>()
                .AddTransient<IFacetLimitParameter, FacetLimitParameter>()
                .AddTransient<IFacetQueryParameter<TDocument>, FacetQueryParameter<TDocument>>()
                .AddTransient<IFacetRangeParameter<TDocument>, FacetRangeParameter<TDocument>>()
                .AddTransient<IFacetSpatialParameter<TDocument>, FacetSpatialParameter<TDocument>>()
                .AddTransient<IFieldsParameter<TDocument>, FieldListParameter<TDocument>>()
                .AddTransient<IFilterParameter<TDocument>, FilterQueryParameter<TDocument>>()
                .AddTransient<ILimitParameter, RowsParameter>()
                .AddTransient<IMinimumShouldMatchParameter, MinimumShouldMatchParameter>()
                .AddTransient<IOffsetParameter, StartParameter>()
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
                .AddTransient<IParameterContainer, ParameterContainer>()
                .AddTransient<ISystemParameter, SystemParameter>();

            return builder;
        }
    }
}
