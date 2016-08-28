using SolrExpress.Core.Search.Parameter;
using SolrExpress.Core.Utility;
using System.Linq;

namespace SolrExpress.Core.Search.ParameterValue
{
    /// <summary>
    /// Multi value parameter
    /// </summary>
    public sealed class Multi : ISearchParameterValue, IValidation
    {
        /// <summary>
        /// Create a multi solr parameter value
        /// </summary>
        /// <param name="conditionType">Condition type</param>
        /// <param name="values">Value array of filter</param>
        public Multi(SolrQueryConditionType conditionType, params ISearchParameterValue[] values)
        {
            Checker.IsNull(values);
            Checker.IsLowerThan(values.Length, 2);

            this.Values = values;
            this.ConditionType = conditionType;
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result generated value</returns>
        public string Execute()
        {
            var expression = string.Join(
                string.Concat(" ", this.ConditionType.ToString().ToUpper(), " "),
                this.Values.Select(q => q.Execute()));

            return $"({expression})";
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

            foreach (var queryParameterValue in this.Values)
            {
                var queryValidation = queryParameterValue as IValidation;

                if (queryValidation == null)
                {
                    continue;
                }

                queryValidation.Validate(out isValid, out errorMessage);

                if (!isValid)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Condition type
        /// </summary>
        public SolrQueryConditionType ConditionType { get; private set; }

        /// <summary>
        /// Value array of filter
        /// </summary>
        public ISearchParameterValue[] Values { get; private set; }
    }
}
