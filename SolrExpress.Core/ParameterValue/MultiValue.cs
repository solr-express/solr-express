using System.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Query;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Multi value parameter
    /// </summary>
    public sealed class MultiValue : IQueryParameterValue, IValidation
    {
        private readonly SolrQueryConditionType _conditionType;
        private readonly IQueryParameterValue[] _values;

        /// <summary>
        /// Create a multi solr parameter value
        /// </summary>
        /// <param name="conditionType">Condition type</param>
        /// <param name="values">Value array of the filter</param>
        public MultiValue(SolrQueryConditionType conditionType, params IQueryParameterValue[] values)
        {
            this._values = values;
            this._conditionType = conditionType;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            return string.Join(this._conditionType.ToString(), this._values.Select(q => q.Execute()));
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

            foreach (var queryParameterValue in this._values)
            {
                var queryValidation = queryParameterValue as IValidation;

                if (queryValidation != null)
                {
                    queryValidation.Validate(out isValid, out errorMessage);

                    if (!isValid)
                    {
                        break;
                    }
                }
            }
        }
    }
}
