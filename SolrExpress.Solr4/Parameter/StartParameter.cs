using SolrExpress.Core.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class StartParameter : IOffsetParameter, IParameter<List<string>>
    {
        /// <summary>
        /// Create a limit parameter
        /// </summary>
        public StartParameter()
        {
        }

        /// <summary>
        /// Create a limit parameter
        /// </summary>
        /// <param name="value">Parameter to include in the query</param>
        public StartParameter(int value)
            : this()
        {
            this.Value = value;
        }

        /// <summary>
        /// True to indicate multiples instance of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "start"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"start={this.Value}");
        }

        /// <summary>
        /// Value of limit
        /// </summary>
        public int Value { get; set; }
    }
}
