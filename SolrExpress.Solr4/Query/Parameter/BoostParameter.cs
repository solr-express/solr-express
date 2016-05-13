using SolrExpress.Core;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Query.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public class BoostParameter<TDocument> : IBoostParameter<TDocument>, IParameter<List<string>>, IValidation
        where TDocument : IDocument
    {
        IQueryParameterValue _query;
        BoostFunctionType _boostFunctionType;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation</param>
        public IBoostParameter<TDocument> Configure(IQueryParameterValue query, BoostFunctionType boostFunctionType)
        {
            Checker.IsNull(query);

            this._query = query;
            this._boostFunctionType = boostFunctionType;

            return this;
        }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var boostFunction = BoostFunctionType.Bf.ToString().ToLower();

            container.Add($"{boostFunction}={this._query.Execute()}");
        }

        /// <summary>
        /// Check for the parameter validation
        /// </summary>
        /// <param name="isValid">True if is valid, otherwise false</param>
        /// <param name="errorMessage">The error message, if applicable</param>
        public void Validate(out bool isValid, out string errorMessage)
        {
            isValid = true;
            errorMessage = string.Empty;

            var queryValidation = this._query as IValidation;

            if (queryValidation != null)
            {
                queryValidation.Validate(out isValid, out errorMessage);
            }
        }
    }
}
