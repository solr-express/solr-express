namespace SolrExpress.Options
{
    /// <summary>
    /// Authentication types
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// No authentication mechanism is used
        /// </summary>
        None,

        /// <summary>
        /// Use basic authentication mechanism
        /// </summary>
        Basic,

        /// <summary>
        /// Use OAuth authentication mechanism
        /// </summary>
        BearerToken,

        /// <summary>
        /// Custom authentication mechanism
        /// </summary>
        Custom
    }
}
