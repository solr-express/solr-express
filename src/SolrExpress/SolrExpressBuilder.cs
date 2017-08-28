using System;
using SolrExpress.Options;

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
        /// Use indicated host address
        /// </summary>
        /// <param name="hostAddress">Host address to be used</param>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> UseHostAddress(string hostAddress)
        {
            this.Options.HostAddress = hostAddress;

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
    }
}
