using SolrExpress.Configuration;
using SolrExpress.Connection;
using SolrExpress.Options;
using System;

namespace SolrExpress
{
    /// <summary>
    /// Builder to control SolrExpress behavior
    /// </summary>
    public sealed class SolrExpressBuilder<TDocument>
        where TDocument : Document
    {
        public SolrExpressBuilder(ISolrExpressServiceProvider<TDocument> serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Use indicated options
        /// </summary>
        /// <param name="configureOptions">Action used to configure the options</param>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> UseOptions(Action<SolrExpressOptions> configureOptions)
        {
            configureOptions.Invoke(this.Options);

            return this;
        }

        /// <summary>
        /// Use indicated custom SOLR connection authentication
        /// </summary>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> UseCustomConnectionAuthentication<TCustomSolrConnectionAuthenticationSettings>()
            where TCustomSolrConnectionAuthenticationSettings : class, ICustomSolrConnectionAuthenticationSettings
        {
            this.ServiceProvider.AddSingleton<ICustomSolrConnectionAuthenticationSettings, TCustomSolrConnectionAuthenticationSettings>();

            return this;
        }

        /// <summary>
        /// Use indicated host address
        /// </summary>
        /// <param name="hostAddress">Host address to be used</param>
        /// <returns>Itself</returns>
        [Obsolete("Use UseOptions(q => q.HostAddress = XXX) or UseOptions(q => q.UseHostAddress(XXX))", false)]
        public SolrExpressBuilder<TDocument> UseHostAddress(string hostAddress)
        {
            this.Options.HostAddress = hostAddress;

            return this;
        }

        /// <summary>
        /// Configure document fields
        /// </summary>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> ConfigureDocument(Action<SolrDocumentConfiguration<TDocument>> configureDocument)
        {
            configureDocument.Invoke(this.DocumentConfiguration);

            return this;
        }

        /// <summary>
        /// Options to control SolrExpress behavior
        /// </summary>
        internal SolrExpressOptions Options { get; } = new SolrExpressOptions();

        /// <summary>
        /// Services provider
        /// </summary>
        internal ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }

        /// <summary>
        /// Solr document configurations
        /// </summary>
        internal SolrDocumentConfiguration<TDocument> DocumentConfiguration { get; } = new SolrDocumentConfiguration<TDocument>();
    }
}
