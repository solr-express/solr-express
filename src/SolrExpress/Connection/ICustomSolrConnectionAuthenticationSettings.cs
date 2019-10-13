using Flurl.Http;

namespace SolrExpress.Connection
{
    /// <summary>
    /// Set custom authentication settings to use in connection to SOLR
    /// </summary>
    public interface ICustomSolrConnectionAuthenticationSettings
    {
        /// <summary>
        /// Configure FlurlClient to use in connection to SOLR
        /// </summary>
        /// <param name="flurlClient">Fluent URL client to configure</param>
#if NETCOREAPP2_1
        void Configure(IFlurlRequest flurlClient);
#else
        void Configure(IFlurlClient flurlClient);
#endif
    }
}
