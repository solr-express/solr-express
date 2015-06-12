using System.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Core.Query;

namespace SolrExpress.Core.ParameterValue
{
    /// <summary>
    /// Multi value parameter
    /// </summary>
    public sealed class MultiValue : IQueryParameterValue
    {
        private readonly string _value;

        /// <summary>
        /// Create a multi solr parameter value
        /// </summary>
        /// <param name="conditionType">Condition type</param>
        /// <param name="values">Value array of the filter</param>
        public MultiValue(SolrQueryConditionType conditionType, params IQueryParameterValue[] values)
        {
            var condition = string.Concat(" ", conditionType.ToString().ToUpper(), " ");

            this._value = string.Join(condition, values.Select(q => q.Execute()));
        }

        /// <summary>
        /// Execute parameter value generator
        /// </summary>
        /// <returns>Result of the value generator</returns>
        public string Execute()
        {
            return _value;
        }
    }
}
