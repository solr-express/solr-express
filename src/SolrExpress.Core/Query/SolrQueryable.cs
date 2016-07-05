using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR queryable
    /// </summary>
    public sealed class SolrQueryable<TDocument>
        where TDocument : IDocument
    {
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
        /// Offset value of collection
        /// </summary>
        private long _offset;

        /// <summary>
        /// Limit value of collection
        /// </summary>
        private long _limit;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve expression</param>
        /// <param name="resolver">Resolver used to resolve classes dependency</param>
        /// <param name="configuration">Configurations about SolrQueriable behavior</param>
        public SolrQueryable(IProvider provider, IResolver resolver, Configuration configuration)
        {
            Checker.IsNull(provider);
            Checker.IsNull(resolver);
            Checker.IsNull(configuration);

            this.Provider = provider;
            this.Resolver = resolver;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Set pagination parameters if it was not set
        /// </summary>
        private void SetPaginationParameters()
        {
            var offsetParameter = (IOffsetParameter)this._parameters.FirstOrDefault(q => q is IOffsetParameter);

            if (offsetParameter == null)
            {
                offsetParameter = this.Resolver.GetInstance<IOffsetParameter>().Configure(0);
                this._parameters.Add(offsetParameter);
            }

            var limitParameter = (ILimitParameter)this._parameters.FirstOrDefault(q => q is ILimitParameter);

            if (limitParameter == null)
            {
                limitParameter = this.Resolver.GetInstance<ILimitParameter>().Configure(10);
                this._parameters.Add(limitParameter);
            }

            this._offset = offsetParameter.Value + 1;
            this._limit = limitParameter.Value;
        }

        /// <summary>
        /// Add a parameters to the query
        /// </summary>
        /// <param name="parameters">Parameters to add in the query</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> Parameter(params IParameter[] parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    this.Parameter(parameter);
                }
            }

            return this;
        }

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameter">Parameter to add in the query</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> Parameter(IParameter parameter)
        {
            Checker.IsNull(parameter);
            var multipleInstances = this._parameters.Any(q => q.GetType() == parameter.GetType()) && !parameter.AllowMultipleInstances;
            Checker.IsTrue<AllowMultipleInstanceOfParameterTypeException>(multipleInstances, parameter.GetType().FullName);

            var parameterValidation = parameter as IValidation;

            var mustValidate = this.Configuration.FailFast && parameterValidation != null;

            if (parameter is IAnyParameter)
            {
                mustValidate = mustValidate && this.Configuration.CheckAnyParameter && parameter is IAnyParameter;
            }

            if (mustValidate)
            {
                bool isValid;
                string errorMessage;

                parameterValidation.Validate(out isValid, out errorMessage);

                Checker.IsTrue<InvalidParameterTypeException>(!isValid, parameterValidation.GetType().FullName, errorMessage);
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
            Checker.IsNull(interceptor);

            this._queryInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> QueryInterceptor<TQueryInterceptor>()
            where TQueryInterceptor : class, IQueryInterceptor, new()
        {
            var interceptor = new TQueryInterceptor();

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
            Checker.IsNull(interceptor);

            this._resultInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> ResultInterceptor<TResultInterceptor>()
            where TResultInterceptor : class, IResultInterceptor, new()
        {
            var interceptor = new TResultInterceptor();

            this._resultInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <returns>Solr result</returns>
        public QueryResult<TDocument> Execute(string handler = null)
        {
            var systemParameter = this.Resolver.GetInstance<ISystemParameter>();
            this._parameters.Add(systemParameter);

            this.SetPaginationParameters();

            var parameterContainer = this.Resolver.GetInstance<IParameterContainer>();
            parameterContainer.AddParameters(this._parameters);
            var query = parameterContainer.Execute();

            this._queryInterceptors.ForEach(q => q.Execute(ref query));

            var json = this.Provider.Get(handler ?? RequestHandler.Select, query);

            this._resultInterceptors.ForEach(q => q.Execute(ref json));

            return new QueryResult<TDocument>(this.Resolver, json, this._offset, this._limit);
        }

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        public Configuration Configuration { get; private set; }

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        public IProvider Provider { get; private set; }

        /// <summary>
        /// Resolver used to resolve classes dependency
        /// </summary>
        public IResolver Resolver { get; private set; }
    }
}
