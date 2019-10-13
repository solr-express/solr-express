namespace SolrExpress.Options
{
    public static class SolrExpressOptionsExtension
    {
        /// <summary>
        /// If true, check Solr connection and load informations about document, otherwise false. Default is false
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions IsLazyInfraValidation(this SolrExpressOptions solrExpressOptions, bool value)
        {
            solrExpressOptions.IsLazyInfraValidation = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// If true, check for possibles fails in the use of the Solr Queriable (using SolrFieldAttribute), otherwise false. Default is true
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions IsFailFast(this SolrExpressOptions solrExpressOptions, bool value)
        {
            solrExpressOptions.FailFast = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// If true, add default query parser (Edismax) in all searchs
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions IsEdismaxQueryParser(this SolrExpressOptions solrExpressOptions, bool value)
        {
            solrExpressOptions.EdismaxQueryParser = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// If true, add default standard query (q.alt) in all searchs
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions IsStandardQuery(this SolrExpressOptions solrExpressOptions, bool value)
        {
            solrExpressOptions.StandardQuery = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// If true, add default field (df) in all searchs
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions IsDefaultField(this SolrExpressOptions solrExpressOptions, bool value)
        {
            solrExpressOptions.DefaultField = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// If true, check for possibles mistakes in use of IAnyParameter. Default is true
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions IsCheckAnyParameter(this SolrExpressOptions solrExpressOptions, bool value)
        {
            solrExpressOptions.CheckAnyParameter = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// SOLR host address
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions HasHostAddress(this SolrExpressOptions solrExpressOptions, string value)
        {
            solrExpressOptions.HostAddress = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// Options to security connection
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions HasSecurity(this SolrExpressOptions solrExpressOptions, SecurityOptions value)
        {
            solrExpressOptions.Security = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// Global prefix name in dynamic field configurations
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions HasGlobalDynamicFieldPrefix(this SolrExpressOptions solrExpressOptions, string value)
        {
            solrExpressOptions.GlobalDynamicFieldPrefix = value;

            return solrExpressOptions;
        }

        /// <summary>
        /// Global suffix name in dynamic field configurations
        /// </summary>
        /// <param name="solrExpressOptions">Options to control SolrExpress behavior</param>
        /// <param name="value">Value to set</param>
        /// <returns>Options to control SolrExpress behavior</returns>
        public static SolrExpressOptions HasGlobalDynamicFieldSuffix(this SolrExpressOptions solrExpressOptions, string value)
        {
            solrExpressOptions.GlobalDynamicFieldSuffix = value;

            return solrExpressOptions;
        }
    }
}
