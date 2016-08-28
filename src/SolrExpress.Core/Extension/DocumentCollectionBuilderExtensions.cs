#if NET40 || NET45
using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Extension
{
    public static class DocumentCollectionBuilderExtensions
    {
        /// <summary>
        /// Add SolrExpress framework services in DI container
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="builder">Options builder action</param>
        /// <returns>Configured DocumentCollectionOptionsBuilder</returns>
        public static DocumentCollectionBuilder<TDocument> AddSolrExpress<TDocument>(this DocumentCollectionBuilder<TDocument> builder)
            where TDocument : IDocument
        {
            Checker.IsNull(builder);

            ApplicationServices.Initialize<NetFrameworkEngine>();

            var builderObj = new DocumentCollectionBuilder<TDocument>();
            builderObj.Create();

            return builderObj;
        }
    }
}
#endif