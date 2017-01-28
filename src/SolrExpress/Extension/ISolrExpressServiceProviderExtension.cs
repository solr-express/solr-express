using SolrExpress.Search.Parameter;

namespace SolrExpress.Extension
{
    public static class ISolrExpressServiceProviderExtension
    {
        /// <summary>
        /// Create a not mapped parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Not mapped parameter</returns>
        public static IAnyParameter<TDocument> Any<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IAnyParameter<TDocument>>();
        }

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Boost parameter</returns>
        public static IBoostParameter<TDocument> Boost<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IBoostParameter<TDocument>>();
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Facet field parameter</returns>
        public static IFacetFieldParameter<TDocument> FacetField<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFacetFieldParameter<TDocument>>();
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Facet limit parameter</returns>
        public static IFacetLimitParameter<TDocument> FacetLimit<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFacetLimitParameter<TDocument>>();
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Facet query parameter</returns>
        public static IFacetQueryParameter<TDocument> FacetQuery<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFacetQueryParameter<TDocument>>();
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Facet range parameter</returns>
        public static IFacetRangeParameter<TDocument> FacetRange<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFacetRangeParameter<TDocument>>();
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Facet range parameter</returns>
        public static IFacetSpatialParameter<TDocument> FacetSpatial<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFacetSpatialParameter<TDocument>>();
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Fields parameter</returns>
        public static IFieldsParameter<TDocument> Fields<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFieldsParameter<TDocument>>();
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Filter parameter</returns>
        public static IFilterParameter<TDocument> Filter<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IFilterParameter<TDocument>>();
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Limit parameter</returns>
        public static ILimitParameter<TDocument> Limit<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<ILimitParameter<TDocument>>();
        }

        /// <summary>
        /// Create a minimum should match
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Minimum should match</returns>
        public static IMinimumShouldMatchParameter<TDocument> MinimumShouldMatch<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IMinimumShouldMatchParameter<TDocument>>();
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Offset parameter</returns>
        public static IOffsetParameter<TDocument> Offset<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IOffsetParameter<TDocument>>();
        }

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Query field parameter</returns>
        public static IQueryFieldParameter<TDocument> QueryField<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IQueryFieldParameter<TDocument>>();
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Query parameter</returns>
        public static IQueryParameter<TDocument> Query<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<IQueryParameter<TDocument>>();
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Sort parameter</returns>
        public static ISortParameter<TDocument> Sort<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<ISortParameter<TDocument>>();
        }

        /// <summary>
        /// Create a sort parameter configured to do a random sort
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Sort parameter configured to do a random sort</returns>
        public static ISortRandomlyParameter<TDocument> SortRandomly<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<ISortRandomlyParameter<TDocument>>();
        }

        /// <summary>
        /// Create a spatial filter parameter
        /// </summary>
        /// <param name="serviceProvider">Dependency injection engine</param>
        /// <returns>Spatial filter parameter</returns>
        public static ISpatialFilterParameter<TDocument> SpatialFilter<TDocument>(this ISolrExpressServiceProvider<TDocument> serviceProvider)
            where TDocument : IDocument
        {
            return serviceProvider.GetService<ISpatialFilterParameter<TDocument>>();
        }
    }
}
