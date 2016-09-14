using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search;
using SolrExpress.Core.Update;

namespace SolrExpress.Core
{
    /// <summary>
    /// Options builder to control SOLR Query behavior
    /// </summary>
    public class DocumentCollectionBuilder<TDocument>
        where TDocument : IDocument
    {
        private DocumentCollectionOptions<TDocument> _options;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        public DocumentCollectionBuilder()
        {
            this._options = new DocumentCollectionOptions<TDocument>();

#if NET40 || NET45
            this.Engine = new NetFrameworkEngine();
#else
            this.Engine = new NetCoreEngine();
#endif
        }

        /// <summary>
        /// Use indicated host address in prepared options
        /// </summary>
        /// <param name="options">Options to be used</param>
        /// <returns>Current instance</returns>
        public DocumentCollectionBuilder<TDocument> UseOptions(DocumentCollectionOptions options)
        {
            var shadowOptions = new DocumentCollectionOptions<TDocument>
            {
                CheckAnyParameter = options.CheckAnyParameter,
                FailFast = options.FailFast
            };

            shadowOptions.GlobalParameters.AddRange(options.GlobalParameters);
            shadowOptions.GlobalQueryInterceptors.AddRange(options.GlobalQueryInterceptors);
            shadowOptions.GlobalResultInterceptors.AddRange(options.GlobalResultInterceptors);

            this._options = shadowOptions;

            return this;
        }

        /// <summary>
        /// Use indicated host address in prepared options
        /// </summary>
        /// <param name="hostAddress">Host address to use in options</param>
        /// <returns>Current instance</returns>
        public DocumentCollectionBuilder<TDocument> UseHostAddress(string hostAddress)
        {
            this._options.HostAddress = hostAddress;

            return this;
        }

        /// <summary>
        /// Create configured instance of DocumentCollection<TDocument>
        /// </summary>
        /// <returns>Instance of DocumentCollection<TDocument></returns>
#if NET40 || NET45
        public DocumentCollection<TDocument> Create()
#else
        internal DocumentCollection<TDocument> Create()
#endif
        {
            var documentCollection = new DocumentCollection<TDocument>(this._options, this.Engine);

            this
                .Engine
                .AddSingleton<ISearchParameterBuilder<TDocument>, SearchParameterBuilder<TDocument>>()
                .AddTransient<ISolrSearch<TDocument>, SolrSearch<TDocument>>()
                .AddTransient<ISolrAtomicUpdate<TDocument>, SolrAtomicUpdate<TDocument>>()
                .AddTransient<IDocumentCollection<TDocument>, DocumentCollection<TDocument>>(documentCollection);

            return documentCollection;
        }

        /// <summary>
        /// Services container
        /// </summary>
        internal IEngine Engine { get; set; }
    }
}
