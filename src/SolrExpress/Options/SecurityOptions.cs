using System;

namespace SolrExpress.Options
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
        [Obsolete("Use BasicAuthentication.UserName", false)]
        public string UserName
        {
            get
            {
                return this.BasicAuthentication.UserName;
            }
            set
            {
                this.BasicAuthentication.UserName = value;
            }
        }

        /// <summary>
        /// Password used in authentication process
        /// </summary>
        [Obsolete("Use BasicAuthentication.Password", false)]
        public string Password
        {
            get
            {
                return this.BasicAuthentication.Password;
            }
            set
            {
                this.BasicAuthentication.Password = value;
            }
        }

        /// <summary>
        /// Options used in basic authentication process
        /// </summary>
        public BasicAuthenticationOptions BasicAuthentication { get; set; } = new BasicAuthenticationOptions();

        /// <summary>
        /// Options used in basic authentication process
        /// </summary>
        public BearerTokenAuthenticationOptions BearerToken { get; set; } = new BearerTokenAuthenticationOptions();
    }
}
