#if NETCOREAPP1_0
using Microsoft.Extensions.DependencyInjection;
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

            var expressionCache = new ExpressionCache<TDocument>();
            ((IExpressionCache<TDocument>)expressionCache).Process();

            services.TryAddSingleton<IEngine>(q => (NetCoreEngine)documentCollection.Engine);

            services
                .AddSingleton<ISearchParameterBuilder<TDocument>>(new SearchParameterBuilder<TDocument>(documentCollection.Engine))
                .AddSingleton<IExpressionCache<TDocument>, ExpressionCache<TDocument>>(q => expressionCache)
                .AddTransient<IDocumentCollection<TDocument>, DocumentCollection<TDocument>>(q => documentCollection);

            return builderObj;
        }
    }
}
#endif