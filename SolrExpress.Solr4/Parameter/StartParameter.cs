using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class StartParameter : IOffsetParameter, IParameter<List<string>>
    {
        private int _value { get; set; }

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
            container.Add($"start={this._value}");
        }
        
        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="value">Value of limit</param>
        /// <returns></returns>
        public IOffsetParameter Configure(int value)
        {
            this._value = value;

            return this;
        }
    }
}
