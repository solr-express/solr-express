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
        private readonly List<Type> _searchItemTypes = new List<Type>();
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

            if (!this.Contains<ISearchItem>())
            {
                this._searchItemTypes.Add(typeof(ISearchItem));
            }
        }

        public void AddRange(IEnumerable<ISearchItem> items)
        {
            var searchItems = items as ISearchItem[] ?? items.ToArray();

            this._searchItems.AddRange(searchItems);

            foreach (var item in searchItems)
            {
                if (!this.Contains<ISearchItem>())
                {
                    this._searchItemTypes.Add(item.GetType());
                }
            }
        }

        public bool Contains(Type searchItemType)
        {
            return this._searchItemTypes.Contains(searchItemType);
        }

        public bool Contains<TSearchItem>()
            where TSearchItem : ISearchItem
        {
            return this._searchItemTypes.Contains(typeof(TSearchItem));
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
