using SolrExpress.Core.DependencyInjection;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR queryable
    /// </summary>
    public sealed class SolrQueryable<TDocument> : ISolrQueryable<TDocument>
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
        /// Handler name used in solr request
        /// </summary>
        private string _handlerName = RequestHandler.Select;

        /// <summary>
        /// SOLR connection
        /// </summary>
        private readonly ISolrConnection _solrConnection;

        /// <summary>
        /// Default constructor of class
        /// </summary>
        /// <param name="options">SolrExpress options</param>
        public SolrQueryable(DocumentCollectionOptions<TDocument> options)
        {
            Checker.IsNull(options);

            this.Options = options;

            this._solrConnection = ApplicationServices.Current.GetService<ISolrConnection>();
            this._solrConnection.SolrHost = this.Options.HostAddress;
        }

        /// <summary>
        /// Set pagination parameters if it was not set
        /// </summary>
        private void SetDefaultPaginationParameters()
        {
            var offsetParameter = (IOffsetParameter)this._parameters.FirstOrDefault(q => q is IOffsetParameter);

            if (offsetParameter == null)
            {
                offsetParameter = ApplicationServices.Current.GetService<IOffsetParameter>().Configure(0);
                this._parameters.Add(offsetParameter);
            }

            var limitParameter = (ILimitParameter)this._parameters.FirstOrDefault(q => q is ILimitParameter);

            if (limitParameter == null)
            {
                limitParameter = ApplicationServices.Current.GetService<ILimitParameter>().Configure(10);
                this._parameters.Add(limitParameter);
            }
        }

        /// <summary>
        /// Add a parameters to the query
        /// </summary>
        /// <param name="parameters">Parameters to add in the query</param>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> Parameter(params IParameter[] parameters)
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
        public ISolrQueryable<TDocument> Parameter(IParameter parameter)
        {
            Checker.IsNull(parameter);
            var multipleInstances = this._parameters.Any(q => q.GetType() == parameter.GetType()) && !parameter.AllowMultipleInstances;
            Checker.IsTrue<AllowMultipleInstanceOfParameterTypeException>(multipleInstances, parameter.GetType().FullName);

            var parameterValidation = parameter as IValidation;

            var mustValidate = this.Options.FailFast && parameterValidation != null;

            if (parameter is IAnyParameter)
            {
                mustValidate = mustValidate && this.Options.CheckAnyParameter && parameter is IAnyParameter;
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
        /// <param name="interceptors">Query interceptors to add in the queryable</param>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> QueryInterceptor(params IQueryInterceptor[] interceptors)
        {
            if (interceptors != null)
            {
                foreach (var interceptor in interceptors)
                {
                    this.QueryInterceptor(interceptor);
                }
            }

            return this;
        }

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <param name="interceptor">Query interceptor to add in the queryable</param>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> QueryInterceptor(IQueryInterceptor interceptor)
        {
            Checker.IsNull(interceptor);

            this._queryInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a query interceptor to the queryable
        /// </summary>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> QueryInterceptor<TQueryInterceptor>()
            where TQueryInterceptor : class, IQueryInterceptor, new()
        {
            var interceptor = new TQueryInterceptor();

            this._queryInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <param name="interceptors">Result interceptors to add in the queryable</param>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> ResultInterceptor(params IResultInterceptor[] interceptors)
        {
            if (interceptors != null)
            {
                foreach (var interceptor in interceptors)
                {
                    this.ResultInterceptor(interceptor);
                }
            }

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <param name="interceptor">The result interceptor to add in the queryable</param>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> ResultInterceptor(IResultInterceptor interceptor)
        {
            Checker.IsNull(interceptor);

            this._resultInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Add a result interceptor to the queryable
        /// </summary>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> ResultInterceptor<TResultInterceptor>()
            where TResultInterceptor : class, IResultInterceptor, new()
        {
            var interceptor = new TResultInterceptor();

            this._resultInterceptors.Add(interceptor);

            return this;
        }

        /// <summary>
        /// Handler name used in solr request
        /// </summary>
        /// <param name="name">Name to be used</param>
        /// <returns>Itself</returns>
        public ISolrQueryable<TDocument> Handler(string name)
        {
            Checker.IsNullOrWhiteSpace(name);

            this._handlerName = name;

            return this;
        }

        /// <summary>
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        public QueryResult<TDocument> Execute()
        {
            var systemParameter = ApplicationServices.Current.GetService<ISystemParameter>();
            var parameterContainer = ApplicationServices.Current.GetService<IParameterContainer>();

            this.Parameter(this.Options.GlobalParameters.ToArray());
            this.QueryInterceptor(this.Options.GlobalQueryInterceptors.ToArray());
            this.ResultInterceptor(this.Options.GlobalResultInterceptors.ToArray());

            this._parameters.Add(systemParameter);

            this.SetDefaultPaginationParameters();

            parameterContainer.AddParameters(this._parameters);
            var query = parameterContainer.Execute();

            this._queryInterceptors.ForEach(q => q.Execute(ref query));

            var json = this._solrConnection.Get(this._handlerName, query);

            this._resultInterceptors.ForEach(q => q.Execute(ref json));

            return new QueryResult<TDocument>(this._parameters, json);
        }

        /// <summary>
        /// SolrExpress options
        /// </summary>
        public DocumentCollectionOptions<TDocument> Options { get; private set; }
    }
}
