using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using System;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR queryable with fluent API
    /// </summary>
    public class SolrQueryable<TDocument> : BaseSolrQueryable<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve the expression</param>
        /// <param name="paramaterFactory">Factory used to resolve parameter creation in Linq facilities</param>
        /// <param name="builderFactory">Factory used to resolve builder creation in Linq facilities</param>
        /// <param name="configuration">Configurations about SolrQueriable behavior</param>
        public SolrQueryable(IProvider provider, IParameterFactory<TDocument> paramaterFactory, IBuilderFactory<TDocument> builderFactory, SolrQueryConfiguration configuration = null)
            : base(provider, paramaterFactory, builderFactory, configuration)
        {
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public SolrQueryable<TDocument> FacetField(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFacetFieldParameter(expression, sortType));
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public SolrQueryable<TDocument> FacetQuery(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFacetQueryParameter(aliasName, query, sortType));
        }

        /// <summary>
        /// Create a facet range parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="gap">Size of each range bucket to make the facet</param>
        /// <param name="start">Lower bound to make the facet</param>
        /// <param name="end">Upper bound to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public SolrQueryable<TDocument> FacetRange(string aliasName, Expression<Func<TDocument, object>> expression, string gap = null, string start = null, string end = null, SolrFacetSortType? sortType = null)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFacetRangeParameter(aliasName, expression, gap, start, end, sortType));
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public SolrQueryable<TDocument> Fields(params Expression<Func<TDocument, object>>[] expressions)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFieldsParameter(expressions));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public SolrQueryable<TDocument> Filter(Expression<Func<TDocument, object>> expression, string value)
        {
            var paramaterValue = new SingleValue<TDocument>(expression, value);
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFilterParameter(paramaterValue));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="from">From value in a range filter</param>
        /// <param name="to">To value in a range filter</param>
        public SolrQueryable<TDocument> Filter<TValue>(Expression<Func<TDocument, object>> expression, TValue? from, TValue? to)
                        where TValue : struct
        {
            var value = new RangeValue<TDocument, TValue>(expression, from, to);
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFilterParameter(value));
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Limit(int value)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetLimitParameter(value));
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Offset(int value)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetOffsetParameter(value));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Query(IQueryParameterValue value)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetQueryParameter(value));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Query(string value)
        {
            var freeValue = new FreeValue(value);
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetQueryParameter(freeValue));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public SolrQueryable<TDocument> Query(Expression<Func<TDocument, object>> expression, string value)
        {
            var paramaterValue = new SingleValue<TDocument>(expression, value);
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetQueryParameter(paramaterValue));
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public SolrQueryable<TDocument> Sort(Expression<Func<TDocument, object>> expression, bool ascendent)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetSortParameter(expression, ascendent));
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> FacetLimit(int value)
        {
            return (SolrQueryable<TDocument>)this.Parameter(this._paramaterFactory.GetFacetLimitParameter(value));
        }
    }
}
