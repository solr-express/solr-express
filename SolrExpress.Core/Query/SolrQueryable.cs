using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR queryable
    /// </summary>
    public sealed class SolrQueryable<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        private readonly Configuration _configuration;

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
            this._configuration = configuration;
        }

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameter">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> Parameter(IParameter parameter)
        {
            Checker.IsNull(parameter);
            var multipleInstances = this._parameters.Any(q => q.GetType() == parameter.GetType()) && !parameter.AllowMultipleInstances;
            Checker.IsTrue<AllowMultipleInstanceOfParameterTypeException>(multipleInstances, parameter.GetType().FullName);

            var parameterValidation = parameter as IValidation;

            var mustValidate = this._configuration.FailFast && parameterValidation != null;

            if (parameter is IAnyParameter)
            {
                mustValidate = mustValidate && this._configuration.CheckAnyParameter && parameter is IAnyParameter;
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
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <param name="handler">Handler name used in solr request</param>
        /// <returns>Solr result</returns>
        public QueryResult<TDocument> Execute(string handler = null)
        {
            var query = this.Provider.GetQueryInstruction(this._parameters);

            this._queryInterceptors.ForEach(q => q.Execute(ref query));

            var requestHandler = string.IsNullOrWhiteSpace(handler) ? RequestHandler.Select : handler;
            var json = this.Provider.Execute(requestHandler, query);

            this._resultInterceptors.ForEach(q => q.Execute(ref json));

            return new QueryResult<TDocument>(this.Resolver, json);
        }

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
