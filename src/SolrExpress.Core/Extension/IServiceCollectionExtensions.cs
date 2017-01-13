#if NETCORE
using Microsoft.Extensions.DependencyInjection;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Update;
using SolrExpress.Core.Utility;
using System;

namespace SolrExpress.Core.Extension
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Add SolrExpress framework services in DI container
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="builder">Options builder action</param>
        /// <returns>Configured DocumentCollectionOptionsBuilder</returns>
        public static DocumentCollectionBuilder<TDocument> AddSolrExpress<TDocument>(this IServiceCollection services, Action<DocumentCollectionBuilder<TDocument>> builder)
            where TDocument : IDocument
        {
            Checker.IsNull(builder);

            var builderObj = new DocumentCollectionBuilder<TDocument>();
            builder.Invoke(builderObj);
            var documentCollection = builderObj.Create();

            var expressionCache = new ExpressionCache<TDocument>();
            var expressionBuilder = new ExpressionBuilder<TDocument>(expressionCache);

            documentCollection
                .Engine
                .AddSingleton<DocumentCollectionOptions<TDocument>, DocumentCollectionOptions<TDocument>>(documentCollection.Options)
                .AddSingleton<ISearchParameterBuilder<TDocument>, SearchParameterBuilder<TDocument>>()
                .AddSingleton<IExpressionCache<TDocument>, ExpressionCache<TDocument>>(expressionCache)
                .AddSingleton<IExpressionBuilder<TDocument>, ExpressionBuilder<TDocument>>(expressionBuilder)
                .AddTransient<ISolrSearch<TDocument>, SolrSearch<TDocument>>()
                .AddTransient<ISolrAtomicUpdate<TDocument>, SolrAtomicUpdate<TDocument>>();

            services
                .AddSingleton<DocumentCollectionOptions<TDocument>, DocumentCollectionOptions<TDocument>>(q => documentCollection.Options)
                .AddSingleton<IEngine>(q => (NetCoreEngine)documentCollection.Engine)
                .AddSingleton(q => expressionBuilder)
                .AddSingleton<ISearchParameterBuilder<TDocument>, SearchParameterBuilder<TDocument>>()
                .AddTransient<IDocumentCollection<TDocument>, DocumentCollection<TDocument>>();

            ExpressionCacheWarmup.Load(expressionBuilder);

            return builderObj;
        }
    }
}
#endif