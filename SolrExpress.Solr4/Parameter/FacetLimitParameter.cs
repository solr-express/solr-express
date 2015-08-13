using SolrExpress.Core.Query;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FacetLimitParameter : IParameter<List<string>>
    {
        private readonly int _value;

        /// <summary>
        /// Create a facet limit parameter
        /// </summary>
        /// <param name="value">Value of the parameter limit</param>
        public FacetLimitParameter(int value)
        {
            this._value = value;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return false; } }

        /// <summary>
        /// Execute the creation of the parameter "rows"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add(string.Concat("facet.limit=", this._value));
        }
    }
}
