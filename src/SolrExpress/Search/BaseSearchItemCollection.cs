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

        public void Add(ISearchItem item)
        {
            this._searchItems.Add(item);
        }

        public bool Contains(Type searchItemType)
        {
            return this._searchItems.Any(q => q.GetType() == searchItemType);
        }

        public bool Contains<TSearchItem>()
            where TSearchItem : ISearchItem
        {
            return this._searchItems.Any(q => q is TSearchItem);
        }

        public List<ISearchParameter> GetSearchParameters()
        {
            return this.GetItems<ISearchParameter>();
        }

        public List<ISearchResult<TDocument>> GetSearchResults()
        {
            return this.GetItems<ISearchResult<TDocument>>();
        }

        public abstract JsonReader Execute(string requestHandler);
    }
}
