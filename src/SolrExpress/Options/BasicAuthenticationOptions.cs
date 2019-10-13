namespace SolrExpress.Options
{
    /// <summary>
    /// Basic security access options
    /// </summary>
    public class BasicAuthenticationOptions
    {
        /// <summary>
        /// User name used in authentication process
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password used in authentication process
        /// </summary>
        public string Password { get; set; }
    }
}
