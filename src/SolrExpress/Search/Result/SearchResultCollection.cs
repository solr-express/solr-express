using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Search.Result
{
    /// <summary>
    /// Search result collection
    /// </summary>
    public sealed class SearchResultCollection<TDocument>
        where TDocument : IDocument
    {
        private List<ISearchResult> _internalList = new List<ISearchResult>();

        /// <summary>
        /// Adds an object to the end of the internal list
        /// </summary>
        /// <param name="item"> The object to be added to the end of the list</param>
        internal void Add(ISearchResult item)
        {
            this._internalList.Add(item);
        }

        /// <summary>
        /// Get list of ISearchResult
        /// </summary>
        /// <returns>List of ISearchResult</returns>
        internal List<ISearchResult> GetList()
        {
            return this._internalList.ToList();
        }
    }
}
