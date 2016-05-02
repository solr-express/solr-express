using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Query.Parameter
{
    public sealed class RowsParameter : ILimitParameter, IParameter<List<string>>
    {
        private int _value;

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "rows"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"rows={this._value}");
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        public ILimitParameter Configure(int value)
        {
            this._value = value;

            return this;
        }
    }
}
