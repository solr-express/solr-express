#if NETCORE
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
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

            services.TryAddSingleton<IEngine>(q => (NetCoreEngine)documentCollection.Engine);

            services
                .AddSingleton<IExpressionCache<TDocument>, ExpressionCache<TDocument>>()
                .AddSingleton<IExpressionBuilder<TDocument>, ExpressionBuilder<TDocument>>()
                .AddSingleton<ISearchParameterBuilder<TDocument>, SearchParameterBuilder<TDocument>>()
                .AddTransient<IDocumentCollection<TDocument>, DocumentCollection<TDocument>>(q => documentCollection);

            var serviceProvider = services.BuildServiceProvider();
            var expressionBuilder = serviceProvider.GetService<IExpressionBuilder<TDocument>>();

            ExpressionCacheWarmup.Load(expressionBuilder);

            return builderObj;
        }
    }
}
#endif