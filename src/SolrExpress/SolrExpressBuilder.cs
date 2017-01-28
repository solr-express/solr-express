namespace SolrExpress
{
    /// <summary>
    /// Builder to control SolrExpress behavior
    /// </summary>
    public sealed class SolrExpressBuilder<TDocument>
        where TDocument : IDocument
    {
        private readonly SolrExpressOptions _options = new SolrExpressOptions();

        /// <summary>
        /// Use indicated options
        /// </summary>
        /// <param name="options">Options to be used</param>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> UseOptions(SolrExpressOptions options)
        {
            this._options.CheckAnyParameter = options.CheckAnyParameter;
            this._options.FailFast = options.FailFast;
            this._options.Security.AuthenticationType = options.Security.AuthenticationType;
            this._options.Security.Password = options.Security.Password;
            this._options.Security.UserName = options.Security.UserName;
            this._options.GlobalParameters.AddRange(options.GlobalParameters);
            this._options.GlobalQueryInterceptors.AddRange(options.GlobalQueryInterceptors);
            this._options.GlobalResultInterceptors.AddRange(options.GlobalResultInterceptors);

            return this;
        }

        /// <summary>
        /// Use indicated host address
        /// </summary>
        /// <param name="hostAddress">Host address to be used</param>
        /// <returns>Itself</returns>
        public SolrExpressBuilder<TDocument> UseHostAddress(string hostAddress)
        {
            this._options.HostAddress = hostAddress;

            return this;
        }
    }
}
