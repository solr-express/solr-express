using Newtonsoft.Json.Linq;
using SolrExpress.Core;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;

namespace SolrExpress.Solr5.Query.Parameter
{
    /// <summary>
    /// Signatures to use boost parameter
    /// </summary>
    public class BoostParameter<TDocument> : IBoostParameter<TDocument>, IParameter<JObject>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Query used to make boost
        /// </summary>
        public IQueryParameterValue Query { get; private set; }

        /// <summary>
        /// Boost type used in calculation
        /// </summary>
        public BoostFunctionType BoostFunctionType { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation</param>
        public IBoostParameter<TDocument> Configure(IQueryParameterValue query, BoostFunctionType boostFunctionType)
        {
            Checker.IsNull(query);

            this.Query = query;
            this.BoostFunctionType = boostFunctionType;

            return this;
        }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            var boostFunction = BoostFunctionType.Bf.ToString().ToLower();

            jObj.Add(new JProperty(boostFunction, this.Query.Execute()));

            jObject["params"] = jObj;
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

            var queryValidation = this.Query as IValidation;

            if (queryValidation != null)
            {
                queryValidation.Validate(out isValid, out errorMessage);
            }
        }
    }
}
