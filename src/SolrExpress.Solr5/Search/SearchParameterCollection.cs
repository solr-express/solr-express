using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Solr5.Search
{
    /// <summary>
    /// Parameter collection
    /// </summary>
    public class SearchParameterCollection : ISearchParameterCollection
    {
        private IEnumerable<ISearchParameter> _parameters;

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameters">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        public void Add(IEnumerable<ISearchParameter> parameters)
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
                ((ISearchParameter<JObject>)item).Execute(jObject);
            }

            return jObject.ToString();
        }
    }
}
