using SolrExpress.Core.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class RowsParameter : ILimitParameter, IParameter<List<string>>
    {
        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of the parameter limit</param>
        public RowsParameter()
        {
        }

        /// <summary>
        /// Create a offset parameter
        /// </summary>
        /// <param name="value">Value of the parameter limit</param>
        public RowsParameter(int value)
            : this()
        {
            this.Value = value;
        }

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
            container.Add($"rows={this.Value}");
        }

        /// <summary>
        /// Value of limit
        /// </summary>
        public int Value { get; set; }
    }
}
