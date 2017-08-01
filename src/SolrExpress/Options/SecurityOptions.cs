namespace SolrExpress
{
    /// <summary>
    /// Security access options
    /// </summary>
    public class SecurityOptions
    {
        /// <summary>
        /// Type of authentication used in process
        /// </summary>
        public AuthenticationType AuthenticationType { get; set; }

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
