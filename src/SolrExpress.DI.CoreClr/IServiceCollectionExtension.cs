using Microsoft.Extensions.DependencyInjection;
using SolrExpress.Utility;
using System;

namespace SolrExpress.DI.CoreClr
{
    /// <summary>
    /// Configure SolrExpress DI using main .Net Core DI engine
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Add SolrExpress services
        /// </summary>
        /// <param name="serviceCollection">Service collection used in .Net Core DI engine</param>
        /// <returns>Service collection used in .Net Core DI engine</returns>
        public static IServiceCollection AddSolrExpress<TDocument>(this IServiceCollection serviceCollection, Action<SolrExpressBuilder<TDocument>> builder)
            where TDocument : IDocument
        {
            var solrExpressServiceProvider = new SolrExpressServiceProvider<TDocument>();
            var solrExpressBuilder = new SolrExpressBuilder<TDocument>(solrExpressServiceProvider);

            serviceCollection
                .AddSingleton<ISolrExpressServiceProvider<TDocument>>(solrExpressServiceProvider)
                .AddSingleton<DocumentCollection<TDocument>>();

            CoreDependecyInjection.Configure(solrExpressServiceProvider, solrExpressBuilder.Options);

            builder.Invoke(solrExpressBuilder);

            return serviceCollection;
        }
    }
}
