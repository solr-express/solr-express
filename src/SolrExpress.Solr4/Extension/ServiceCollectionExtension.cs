//#if NETCOREAPP1_0
//using Microsoft.Extensions.DependencyInjection;
//using SolrExpress.Core;
//using SolrExpress.Core.Query;
//using SolrExpress.Core.Query.Parameter;
//using SolrExpress.Core.Query.Result;
//using SolrExpress.Core.Update;
//using SolrExpress.Solr4.Query;
//using SolrExpress.Solr4.Query.Parameter;
//using SolrExpress.Solr4.Query.Result;
//using SolrExpress.Solr4.Update;
//using System;

//namespace SolrExpress.Solr4.Extension
//{
//    public static class ServiceCollectionExtension
//    {
//        public static IServiceCollection AddSolr4(this IServiceCollection services, Action<DocumentCollectionOptions> options)
//        {
//            if (options != null)
//            {
//                services.Configure(options);
//            }

//            //var serviceDescriptor = new ServiceDescriptor(typeof(IBankManager), typeof(BankManager), ServiceLifetime.Transient);
//            //services.Add(serviceDescriptor);

//            services.AddTransient<IAnyParameter, AnyParameter>();
//            services.AddTransient<IFacetFieldParameter<>, FacetFieldParameter<>>();
//            services.AddTransient<IFacetLimitParameter, FacetLimitParameter>();
//            services.AddTransient<IFacetQueryParameter<>, FacetQueryParameter<>>();
//            services.AddTransient<IFacetRangeParameter<>, FacetRangeParameter<>>();
//            services.AddTransient<IFacetSpatialParameter<>, FacetSpatialParameter<>>();
//            services.AddTransient<IFieldsParameter<>, FieldListParameter<>>();
//            services.AddTransient<IFilterParameter<>, FilterQueryParameter<>>();
//            services.AddTransient<ILimitParameter, RowsParameter>();
//            services.AddTransient<IMinimumShouldMatchParameter, MinimumShouldMatchParameter>();
//            services.AddTransient<IOffsetParameter, StartParameter>();
//            services.AddTransient<IQueryFieldParameter, QueryFieldParameter>();
//            services.AddTransient<IQueryParameter<>, QueryParameter<>>();
//            services.AddTransient<ISortParameter<>, SortParameter<>>();
//            services.AddTransient<IRandomSortParameter, RandomSortParameter>();
//            services.AddTransient<ISpatialFilterParameter<>, SpatialFilterParameter<>>();
//            services.AddTransient<IBoostParameter<>, BoostParameter<>>();

//            services.AddTransient<IDocumentResult<>, DocumentResult<>>();
//            services.AddTransient<IFacetFieldResult<>, FacetFieldResult<>>();
//            services.AddTransient<IFacetQueryResult<>, FacetQueryResult<>>();
//            services.AddTransient<IFacetRangeResult<>, FacetRangeResult<>>();
//            services.AddTransient<IInformationResult<>, InformationResult<>>();

//            services.AddTransient<IAtomicUpdate<>, AtomicUpdate<>>();
//            services.AddTransient<IAtomicDelete<>, AtomicDelete<>>();

//            services.AddTransient<IParameterContainer, ParameterContainer>();

//            services.AddTransient<ISystemParameter, SystemParameter>();

//            return services;
//        }
//    }
//}
//#endif