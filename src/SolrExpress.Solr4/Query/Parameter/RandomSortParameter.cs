using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr4.Query.Parameter.Internal;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Query.Parameter
{
    public class RandomSortParameter : IRandomSortParameter, IParameter<List<string>>
    {
        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// True to ascendent order, otherwise false
        /// </summary>
        public bool Ascendent { get; private set; }

        /// <summary>
        /// Execute creation of parameter "sort"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            var command = new SortCommand();
            command.Execute("random", this.Ascendent, container);
        }

        /// <summary>
        /// Configure current instance
        /// </summary>
        /// <param name="ascendent">True to ascendent order, otherwise false</param>
        public IRandomSortParameter Configure(bool ascendent)
        {
            this.Ascendent = ascendent;

            return this;
        }
    }
}
