using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Search.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Core.Search
{
    /// <summary>
    /// Signatures to search parameter builder
    /// </summary>
    public interface ISearchParameterBuilder<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="limit">Limit of itens in facet's result</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetFieldParameter<TDocument> FacetField(Expression<Func<TDocument, object>> expression, FacetSortType? sortType = null, int? limit = null, params string[] excludes);

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        /// <param name="excludes">List of tags to exclude in facet calculation</param>
        IFacetQueryParameter<TDocument> FacetQuery(string aliasName, ISearchParameterValue<TDocument> query, FacetSortType? sortType = null, params string[] excludes);

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        IFacetRangeParameter<TDocument> FacetRange(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, FacetSortType? sortType = null);

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        IFieldsParameter<TDocument> Fields(params Expression<Func<TDocument, object>>[] expressions);

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter<TDocument> Filter(Expression<Func<TDocument, object>> expression, string value, string tagName = null);

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        IFilterParameter<TDocument> Filter<TValue>(Expression<Func<TDocument, object>> expression, TValue? from = null, TValue? to = null, string tagName = null)
            where TValue : struct;

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        ILimitParameter<TDocument> Limit(int value);

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        IOffsetParameter<TDocument> Offset(int value);

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IQueryParameter<TDocument> Query(ISearchParameterValue<TDocument> value);

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        IQueryParameter<TDocument> Query(string value);

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        IQueryParameter<TDocument> Query(Expression<Func<TDocument, object>> expression, string value);

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        ISortParameter<TDocument> Sort(Expression<Func<TDocument, object>> expression, bool ascendent);

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        IEnumerable<ISortParameter<TDocument>> Sort(bool ascendent, params Expression<Func<TDocument, object>>[] expressions);

        /// <summary>
        /// Create a random sort parameter
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        /// <param name="expressions">Expression used to find the property name</param>
        IRandomSortParameter<TDocument> RandomSort(bool ascendent);

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of limit</param>
        IFacetLimitParameter<TDocument> FacetLimit(int value);

        /// <summary>
        /// Create a minimum should match parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        IMinimumShouldMatchParameter<TDocument> MinimumShouldMatch(string expression);

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        IQueryFieldParameter<TDocument> QueryField(string expression);

        /// <summary>
        /// Create a query field parameter using spatial notation
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="functionType">Function used in the spatial filter</param>
        /// <param name="centerPoint">Center point to spatial filter</param>
        /// <param name="distance">Distance from the center point</param>
        ISpatialFilterParameter<TDocument> SpatialFilter(Expression<Func<TDocument, object>> expression, SolrSpatialFunctionType functionType, GeoCoordinate centerPoint, decimal distance);

        /// <summary>
        /// Create a any parameter
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        IAnyParameter<TDocument> Any(string name, string value);

        /// <summary>
        /// Create a boost parameter
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation. Default is BoostFunctionType.Boost</param>
        IBoostParameter<TDocument> Boost(ISearchParameterValue<TDocument> query, BoostFunctionType? boostFunctionType = null);

        /// <summary>
        /// Get current instance of ServiceContainer
        /// </summary>
        /// <returns>Instance of ServiceContainer</returns>
        IEngine Engine { get; }
    }
}
