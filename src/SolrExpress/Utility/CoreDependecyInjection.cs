using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Behaviour;
using SolrExpress.Search.Result;
using SolrExpress.Update;

namespace SolrExpress.Utility
{
    /// <summary>
    /// Helper class used to add core services in DI engine
    /// </summary>
    internal static class CoreDependecyInjection
    {
        /// <summary>
        /// Configure core services
        /// </summary>
        /// <param name="serviceProvider">DI engine to be configured</param>
        /// <param name="options">Options to control SolrExpress behavior</param>
        /// </summary>
        internal static void Configure<TDocument>(ISolrExpressServiceProvider<TDocument> serviceProvider, SolrExpressOptions options)
            where TDocument : IDocument
        {
            var expressionBuilder = new ExpressionBuilder<TDocument>(options);
            expressionBuilder.ProcessDocument();

            serviceProvider
                .AddSingleton(options)
                .AddTransient(serviceProvider)
                .AddTransient<SolrConnection>()
                .AddTransient<DocumentSearch<TDocument>>()
                .AddTransient<DocumentUpdate<TDocument>>()
                .AddTransient<SearchResultBuilder<TDocument>>()
                .AddTransient(expressionBuilder)
                .AddTransient<IDocumentResult<TDocument>, DocumentResult<TDocument>>()
                .AddTransient<IInformationResult<TDocument>, InformationResult<TDocument>>()
                .AddTransient<IChangeDynamicFieldBehaviour<TDocument>, ChangeDynamicFieldBehaviour<TDocument>>();
        }
    }
}
