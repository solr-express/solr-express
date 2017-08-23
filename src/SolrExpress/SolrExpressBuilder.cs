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
            this.Options = new SolrExpressOptions();
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Use indicated options
        /// </summary>
        /// <param name="options">Options to be used</param>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> UseOptions(SolrExpressOptions options)
        {
            this.Options.CheckAnyParameter = options.CheckAnyParameter;
            this.Options.FailFast = options.FailFast;
            this.Options.GlobalDynamicFieldPrefix = options.GlobalDynamicFieldPrefix;
            this.Options.GlobalDynamicFieldSuffix = options.GlobalDynamicFieldSuffix;
            this.Options.Security.AuthenticationType = options.Security.AuthenticationType;
            this.Options.Security.Password = options.Security.Password;
            this.Options.Security.UserName = options.Security.UserName;
            this.Options.GlobalParameters.AddRange(options.GlobalParameters);
            this.Options.GlobalResultInterceptors.AddRange(options.GlobalResultInterceptors);
            this.Options.GlobalChangeBehaviours.AddRange(options.GlobalChangeBehaviours);

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
        internal SolrExpressOptions Options { get; set; }

        /// <summary>
        /// Services provider
        /// </summary>
        internal ISolrExpressServiceProvider<TDocument> ServiceProvider { get; set; }
    }
}
