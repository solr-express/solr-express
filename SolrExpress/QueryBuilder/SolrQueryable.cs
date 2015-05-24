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
        #region Constructor

        /// <summary>
        /// Constructor used to informed the SolrProvider to the instance
        /// </summary>
        /// <param name="provider">Provider used to resolve the expression</param>
        public SolrQueryable(IProvider provider, IResultDataBuilder<TDocument> resultDataBuilder)
        {
            this._provider = provider;
            this._resultDataBuilder = resultDataBuilder;
        }

        #endregion Constructor

        #region Private atributes

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
        /// Result data builder
        /// </summary>
        private IResultDataBuilder<TDocument> _resultDataBuilder;

        #endregion Private atributes

        #region Private methods

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

        #endregion Private methods

        #region Public methods

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
        /// Get JSON correspondent of the result of the expression generated
        /// </summary>
        /// <returns>JSON string</returns>
        public string GetJson()
        {
            this.ProcessParameters();

            return this._provider.Execute(this._expression);
        }

        /// <summary>
        /// Get list of the documents correspondent of the result of the expression generated
        /// </summary>
        /// <returns>List of the documents</returns>
        public List<TDocument> Execute()
        {
            var json = this.GetJson();

            var jsonObject = JObject.Parse(json);

            return this._resultDataBuilder.Execute(jsonObject);
        }

        #endregion Public methods
    }
}
