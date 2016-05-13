using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr4.Query
{
    /// <summary>
    /// Contain parameters
    /// </summary>
    public class ParameterContainer : IParameterContainer
    {
        private List<IParameter> _parameters;

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameters">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        public void AddParameters(List<IParameter> parameters)
        {
            this._parameters = parameters;
        }

        /// <summary>
        /// Execute parameters and get query instructions
        /// </summary>
        /// <returns>Query instructions</returns>
        public string Execute()
        {
            var list = new List<string>();

            foreach (var item in this._parameters?.OrderBy(q => q.GetType().ToString()))
            {
                ((IParameter<List<string>>)item).Execute(list);
            }

            return string.Join("&", list);
        }
    }
}
