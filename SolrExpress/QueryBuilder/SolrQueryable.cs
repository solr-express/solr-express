using Newtonsoft.Json.Linq;
using SolrExpress.Exception;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.QueryBuilder
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
        private SortedDictionary<string, List<IQueryParameter>> _parameterGroups = new SortedDictionary<string, List<IQueryParameter>>();

        /// <summary>
        /// Expression created basead in the commands triggereds
        /// </summary>
        private string _expression;

        /// <summary>
        /// Provider used to resolve the expression
        /// </summary>
        private IProvider _provider;

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

            foreach (var parameterGroup in this._parameterGroups)
            {
                foreach (var parameter in parameterGroup.Value)
                {
                    parameter.Execute(jsonObj);
                }
            }

            this._expression = jsonObj.ToString();
        }

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameter">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        public SolrQueryable<TDocument> Add(IQueryParameter parameter)
        {
            if (!this._parameterGroups.ContainsKey(parameter.ParameterName))
            {
                this._parameterGroups.Add(parameter.ParameterName, new List<IQueryParameter>());
            }

            if (this._parameterGroups[parameter.ParameterName].Any() && !parameter.AllowMultipleInstance)
            {
                throw new AllowMultipleInstanceOfParameterType(parameter.ParameterName);
            }

            this._parameterGroups[parameter.ParameterName].Add(parameter);

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

            var jsonObject = JObject.Parse(json);

            return new SolrQueryResult(jsonObject);
        }
    }
}
