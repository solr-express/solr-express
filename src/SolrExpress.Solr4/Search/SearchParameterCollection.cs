using SolrExpress.Core;
using SolrExpress.Core.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolrExpress.Solr4.Search
{
    /// <summary>
    /// Parameter collection
    /// </summary>
    public class SearchParameterCollection<TDocument> : ISearchParameterCollection<TDocument>
        where TDocument : IDocument
    {
        private IEnumerable<ISearchParameter> _parameters;

        /// <summary>
        /// Add a parameter to the query
        /// </summary>
        /// <param name="parameters">The parameter to add in the query</param>
        /// <returns>Itself</returns>
        void ISearchParameterCollection<TDocument>.Add(IEnumerable<ISearchParameter> parameters)
        {
            this._parameters = parameters;
        }

        /// <summary>
        /// Execute parameters and get query instructions
        /// </summary>
        /// <returns>Query instructions</returns>
        string ISearchParameterCollection<TDocument>.Execute()
        {
            var list = new List<string>();

            if (this._parameters != null)
            {
                Parallel.ForEach(this._parameters, item =>
                {
                    lock (list)
                    {
                        ((ISearchParameterExecute<List<string>>)item).Execute(list);
                    }
                });
            }

            return string.Join("&", list);
        }
    }
}
