namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Extensions to configure in facet query parameter
    /// </summary>
    public static class IFacetQueryParameterExtension
    {
        /// <summary>
        /// Configure name of alias added in query
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="aliasName">Name of alias added in query</param>
        public static IFacetQueryParameter<TDocument> AliasName<TDocument>(this IFacetQueryParameter<TDocument> parameter, string aliasName)
            where TDocument : IDocument
        {
            parameter.AliasName = aliasName;

            return parameter;
        }

        /// <summary>
        /// Configure query used to make facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="query">Query used to make facet</param>
        public static IFacetQueryParameter<TDocument> Query<TDocument>(this IFacetQueryParameter<TDocument> parameter, ISearchQuery<TDocument> query)
            where TDocument : IDocument
        {
            parameter.Query = query;

            return parameter;
        }

        /// <summary>
        /// Configure sort type of result of facet
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="sortType">Sort type of result of facet</param>
        public static IFacetQueryParameter<TDocument> SortType<TDocument>(this IFacetQueryParameter<TDocument> parameter, FacetSortType sortType)
            where TDocument : IDocument
        {
            parameter.SortType = sortType;

            return parameter;
        }

        /// <summary>
        /// Configure list of tags to exclude in facet calculation
        /// </summary>
		/// <param name="parameter">Parameter to congigure</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        public static IFacetQueryParameter<TDocument> Excludes<TDocument>(this IFacetQueryParameter<TDocument> parameter, params string[] excludes)
            where TDocument : IDocument
        {
            parameter.Excludes = excludes;

            return parameter;
        }
    }
}
