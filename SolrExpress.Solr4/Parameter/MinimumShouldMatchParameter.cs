using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class MinimumShouldMatchParameter : IParameter<List<string>>
    {
        private readonly string _expression;

        /// <summary>
        /// Create a minimun should parameter parameter
        /// </summary>
        /// <param name="expression">Expression used to make the mm parameter</param>
        public MinimumShouldMatchParameter(string expression)
        {
            ThrowHelper<ArgumentNullException>.If(string.IsNullOrWhiteSpace(expression));

            this._expression = expression;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = false;

        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"mm={this._expression}");
        }
    }
}
