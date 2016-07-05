using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;
using SolrExpress.Core.Query.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Query
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
            var jObject = new JObject();

            foreach (var item in this._parameters?.OrderBy(q => q.GetType().ToString()))
            {
                ((IParameter<JObject>)item).Execute(jObject);
            }

            return jObject.ToString();
        }
    }
}
