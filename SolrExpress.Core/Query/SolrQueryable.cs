using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.ParameterValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR queryable with fluent API
    /// </summary>
    public class SolrQueryable<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        private readonly SolrQueryConfiguration _configuration;

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        private readonly IProvider _provider;

        /// <summary>
        /// List of the parameters arranged in the queryable class
        /// </summary>
        private readonly List<IParameter> _parameters = new List<IParameter>();

        /// <summary>
        /// List of the query interceptors arranged in the queryable class
        /// </summary>
        private readonly List<IQueryInterceptor> _queryInterceptors = new List<IQueryInterceptor>();

        /// <summary>
        /// List of the result interceptors arranged in the queryable class
        /// </summary>
        private readonly List<IResultInterceptor> _resultInterceptors = new List<IResultInterceptor>();

        /// <summary>
        /// Factory used to resolve builder creation in Linq facilities
        /// </summary>
        protected readonly IBuilderFactory<TDocument> _builderFactory;

        /// <summary>
        /// Factory used to resolve parameter creation in Linq facilities
        /// </summary>
        protected readonly IParameterFactory<TDocument> _paramaterFactory;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve the expression</param>
        /// <param name="paramaterFactory">Factory used to resolve parameter creation in Linq facilities</param>
        /// <param name="builderFactory">Factory used to resolve builder creation in Linq facilities</param>
        /// <param name="configuration">Configurations about SolrQueriable behavior</param>
        public SolrQueryable(IProvider provider, IParameterFactory<TDocument> paramaterFactory, IBuilderFactory<TDocument> builderFactory, SolrQueryConfiguration configuration = null)
        {
            ThrowHelper<ArgumentNullException>.If(provider == null);
            ThrowHelper<ArgumentNullException>.If(paramaterFactory == null);
            ThrowHelper<ArgumentNullException>.If(builderFactory == null);

            this._provider = provider;
            this._paramaterFactory = paramaterFactory;
            this._builderFactory = builderFactory;
            this._configuration = configuration ?? new SolrQueryConfiguration
            {
                FailFast = true
            };
        }

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameter">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> Parameter(IParameter parameter)
        {
            ThrowHelper<ArgumentNullException>.If(parameter == null);

            if (this._parameters.Any(q => q.GetType() == parameter.GetType()) && !parameter.AllowMultipleInstances)
            {
                throw new AllowMultipleInstanceOfParameterTypeException(parameter.ToString());
            }

            var parameterValidation = parameter as IValidation;

            if (this._configuration.FailFast && parameterValidation != null)
            {
                bool isValid;
                string errorMessage;

                parameterValidation.Validate(out isValid, out errorMessage);

                if (!isValid)
                {
                    throw new InvalidParameterTypeException(parameterValidation.ToString(), errorMessage);
                }
            }

            this._parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <param name="interceptor">The query interceptor to add in the queryable</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> QueryInterceptor(IQueryInterceptor interceptor)
        {
            ThrowHelper<ArgumentNullException>.If(interceptor == null);

            this._queryInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <param name="interceptor">The result interceptor to add in the queryable</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> ResultInterceptor(IResultInterceptor interceptor)
        {
            ThrowHelper<ArgumentNullException>.If(interceptor == null);

            this._resultInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        public SolrQueryResult<TDocument> Execute()
        {
            var query = this._provider.GetQuery(this._parameters);

            this._queryInterceptors.ForEach(q => q.Execute(ref query));

            var json = this._provider.Execute(query);

            this._resultInterceptors.ForEach(q => q.Execute(ref json));

            return new SolrQueryResult<TDocument>(this._builderFactory, json);
        }

        /// <summary>
        /// Create a facet field parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public SolrQueryable<TDocument> FacetField(Expression<Func<TDocument, object>> expression, SolrFacetSortType? sortType = null)
        {
            return this.Parameter(this._paramaterFactory.GetFacetFieldParameter(expression, sortType));
        }

        /// <summary>
        /// Create a facet query parameter
        /// </summary>
        /// <param name="aliasName">Name of the alias added in the query</param>
        /// <param name="query">Query used to make the facet</param>
        /// <param name="sortType">Sort type of the result of the facet</param>
        public SolrQueryable<TDocument> FacetQuery(string aliasName, IQueryParameterValue query, SolrFacetSortType? sortType = null)
        {
            return this.Parameter(this._paramaterFactory.GetFacetQueryParameter(aliasName, query, sortType));
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
            return this.Parameter(this._paramaterFactory.GetFacetRangeParameter(aliasName, expression, gap, start, end, sortType));
        }

        /// <summary>
        /// Create a fields parameter
        /// </summary>
        /// <param name="expressions">Expression used to find the property name</param>
        public SolrQueryable<TDocument> Fields(params Expression<Func<TDocument, object>>[] expressions)
        {
            return this.Parameter(this._paramaterFactory.GetFieldsParameter(expressions));
        }

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the filter</param>
        public SolrQueryable<TDocument> Filter(Expression<Func<TDocument, object>> expression, string value)
        {
            var paramaterValue = new SingleValue<TDocument>(expression, value);
            return this.Parameter(this._paramaterFactory.GetFilterParameter(paramaterValue));
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
            return this.Parameter(this._paramaterFactory.GetFilterParameter(value));
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Limit(int value)
        {
            return this.Parameter(this._paramaterFactory.GetLimitParameter(value));
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Offset(int value)
        {
            return this.Parameter(this._paramaterFactory.GetOffsetParameter(value));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Query(IQueryParameterValue value)
        {
            return this.Parameter(this._paramaterFactory.GetQueryParameter(value));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> Query(string value)
        {
            var freeValue = new FreeValue(value);
            return this.Parameter(this._paramaterFactory.GetQueryParameter(freeValue));
        }

        /// <summary>
        /// Create a query parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="value">Value of the query</param>
        public SolrQueryable<TDocument> Query(Expression<Func<TDocument, object>> expression, string value)
        {
            var paramaterValue = new SingleValue<TDocument>(expression, value);
            return this.Parameter(this._paramaterFactory.GetQueryParameter(paramaterValue));
        }

        /// <summary>
        /// Create a sort parameter
        /// </summary>
        /// <param name="expression">Expression used to find the property name</param>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public SolrQueryable<TDocument> Sort(Expression<Func<TDocument, object>> expression, bool ascendent)
        {
            return this.Parameter(this._paramaterFactory.GetSortParameter(expression, ascendent));
        }

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public SolrQueryable<TDocument> FacetLimit(int value)
        {
            return this.Parameter(this._paramaterFactory.GetFacetLimitParameter(value));
        }
    }
}
