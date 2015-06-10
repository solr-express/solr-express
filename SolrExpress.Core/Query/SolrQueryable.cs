using Newtonsoft.Json.Linq;
using SolrExpress.Core.Exception;
using System.Collections.Generic;
using System.Linq;

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
        /// Expression created based in the commands triggereds
        /// </summary>
        private string _expression;

        /// <summary>
        /// Default constructor of the class
        /// </summary>
        /// <param name="provider">Provider used to resolve the expression</param>
        public SolrQueryable(IProvider provider)
        {
            this._provider = provider;
        }

        /// <summary>
        /// Process the queryable class
        /// </summary>
        private void ProcessParameters()
        {
            var jsonObj = new JObject();

            foreach (var item in this._parameters.OrderBy(q => q.GetType().ToString()))
            {
                item.Execute(jsonObj);
            }

            this._expression = jsonObj.ToString();
        }

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameter">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> Parameter(IParameter parameter)
        {
            if (this._parameters.Any(q => q.GetType().Equals(parameter.GetType())) && !parameter.AllowMultipleInstances)
            {
                throw new AllowMultipleInstanceOfParameterTypeException(parameter.ToString());
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
            this.ProcessParameters();

            var json = this._provider.Execute(this._expression);

            return new SolrQueryResult(json);
        }
    }
}
