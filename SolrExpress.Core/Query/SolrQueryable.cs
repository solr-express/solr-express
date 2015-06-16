using System.Collections.Generic;
using System.Linq;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;

namespace SolrExpress.Core.Query
{
    /// <summary>
    /// SOLR queryable
    /// </summary>
    public class SolrQueryable<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// List of the parameters arranged in the queryable class
        /// </summary>
        private readonly List<IParameter> _parameters = new List<IParameter>();

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        private readonly IProvider _provider;

        /// <summary>
        /// Configurations about SolrQueriable behavior
        /// </summary>
        private readonly SolrQueryConfiguration _configuration;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve the expression</param>
        /// <param name="configuration">Configurations about SolrQueriable behavior</param>
        public SolrQueryable(IProvider provider, SolrQueryConfiguration configuration = null)
        {
            this._provider = provider;
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
            if (this._parameters.Any(q => q.GetType() == parameter.GetType()) && !parameter.AllowMultipleInstances)
            {
                throw new AllowMultipleInstanceOfParameterTypeException(parameter.ToString());
            }

            var parameterValidation = parameter as IValidation;

            //TODO: Unit test to _configuration.FailFast
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
        /// Execute the search in the solr with informed parameters
        /// </summary>
        /// <returns>Solr result</returns>
        public SolrQueryResult Execute()
        {
            var json = this._provider.Execute(this._parameters);

            return new SolrQueryResult(json);
        }
    }
}
