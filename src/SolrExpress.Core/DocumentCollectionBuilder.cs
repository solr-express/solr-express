using SolrExpress.Core.DependencyInjection;

namespace SolrExpress.Core
{
    /// <summary>
    /// Options builder to control SOLR Query behavior
    /// </summary>
    public class DocumentCollectionBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Use indicated host address in prepared options
        /// </summary>
        /// <param name="options">Options to be used</param>
        /// <returns>Current instance</returns>
        public DocumentCollectionBuilder<TDocument> UseOptions(DocumentCollectionOptions<TDocument> options)
        {
            var shadowOptions = new DocumentCollectionOptions<TDocument>
            {
                CheckAnyParameter = options.CheckAnyParameter,
                FailFast = options.FailFast
            };

            shadowOptions.GlobalParameters.AddRange(options.GlobalParameters);
            shadowOptions.GlobalQueryInterceptors.AddRange(options.GlobalQueryInterceptors);
            shadowOptions.GlobalResultInterceptors.AddRange(options.GlobalResultInterceptors);

            this.Options = shadowOptions;

            return this;
        }

        /// <summary>
        /// Use indicated host address in prepared options
        /// </summary>
        /// <param name="hostAddress">Host address to use in options</param>
        /// <returns>Current instance</returns>
        public DocumentCollectionBuilder<TDocument> UseHostAddress(string hostAddress)
        {
            this.Options.HostAddress = hostAddress;

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
            return new DocumentCollection<TDocument>(this.Options, this.Engine);
        }

        /// <summary>
        /// Services container
        /// </summary>
        internal IEngine Engine { get; set; }

        /// <summary>
        /// Options to control SOLR Query behavior
        /// </summary>
        internal DocumentCollectionOptions<TDocument> Options { get; private set; } = new DocumentCollectionOptions<TDocument>();
    }
}
