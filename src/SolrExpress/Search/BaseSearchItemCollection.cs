using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
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
            return this._searchItems.Any(q => q.GetType().Equals(searchItemType));
        }

        bool ISearchItemCollection<TDocument>.Contains<ISearchItem>()
        {
            return this._searchItems.Any(q => q is ISearchItem);
        }

        List<ISearchParameter> ISearchItemCollection<TDocument>.GetParameters()
        {
            return this.GetItems<ISearchParameter>();
        }

        public abstract JsonReader Execute(string requestHandler);
    }
}
