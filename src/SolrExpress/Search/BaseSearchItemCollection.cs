using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Search
{
    /// <summary>
    /// Generic parameter collection
    /// </summary>
    public abstract class BaseSearchItemCollection<TDocument> : ISearchItemCollection<TDocument>
        where TDocument : Document
    {
        private readonly List<ISearchItem> _searchItems = new List<ISearchItem>();

        /// <summary>
        /// Get items from internal search items list filtered by indicated type
        /// </summary>
        /// <typeparam name="T">Type to filter</typeparam>
        /// <returns>Filtered search items</returns>
        protected List<T> GetItems<T>()
            where T : class
        {
            return this
                ._searchItems
                .Select(q => q as T)
                .Where(q => q != null)
                .ToList();
        }

        void ISearchItemCollection<TDocument>.Add(ISearchItem item)
        {
            this._searchItems.Add(item);
        }

        bool ISearchItemCollection<TDocument>.Contains(Type searchItemType)
        {
            return this._searchItems.Any(q => q.GetType() == searchItemType);
        }

        bool ISearchItemCollection<TDocument>.Contains<TSearchItem>()
        {
            return this._searchItems.Any(q => q is TSearchItem);
        }

        List<ISearchParameter> ISearchItemCollection<TDocument>.GetSearchParameters()
        {
            return this.GetItems<ISearchParameter>();
        }

        List<ISearchResult<TDocument>> ISearchItemCollection<TDocument>.GetSearchResults()
        {
            return this.GetItems<ISearchResult<TDocument>>();
        }

        public abstract JsonReader Execute(string requestHandler);
    }
}
