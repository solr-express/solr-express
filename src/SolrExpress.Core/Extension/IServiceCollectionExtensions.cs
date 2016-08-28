#if NETCOREAPP1_0
using Microsoft.Extensions.DependencyInjection;
using SolrExpress.Core.DependencyInjection;
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

            ApplicationServices.Initialize<NetCoreEngine>(q => q.SetServiceCollection(services));

            var builderObj = new DocumentCollectionBuilder<TDocument>();
            builderObj.Create();
            builder.Invoke(builderObj);

            return builderObj;
        }
    }
}
#endif