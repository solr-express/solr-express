using SolrExpress.Core.Utility;

namespace SolrExpress.Core.Search.Parameter
{
    public abstract class BaseBoostParameter<TDocument> : IBoostParameter<TDocument>, IValidation
        where TDocument : IDocument
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Query used to make boost
        /// </summary>
        public ISearchParameterValue Query { get; private set; }

        /// <summary>
        /// Boost type used in calculation
        /// </summary>
        public BoostFunctionType BoostFunctionType { get; private set; }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="query">Query used to make boost</param>
        /// <param name="boostFunctionType">Boost type used in calculation</param>
        public IBoostParameter<TDocument> Configure(ISearchParameterValue query, BoostFunctionType boostFunctionType)
        {
            Checker.IsNull(query);

            this.Query = query;
            this.BoostFunctionType = boostFunctionType;

            return this;
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
            queryValidation?.Validate(out isValid, out errorMessage);
        }
    }
}
