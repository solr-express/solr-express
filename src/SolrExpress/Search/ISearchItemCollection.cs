using Newtonsoft.Json;
using SolrExpress.Search.Parameter;
using System;
using System.Collections.Generic;

namespace SolrExpress.Search
{
    /// <summary>
    /// Signature used to parameter collection
    /// </summary>
    public interface ISearchItemCollection<TDocument>
        where TDocument : IDocument
    {
        /// <summary>
        /// Get all parameters in internal list
        /// </summary>
        /// <returns>Parameters in internal list</returns>
        List<ISearchParameter> GetParameters();

        /// <summary>
        /// Check if collection contains informed type
        /// </summary>
        /// <returns>True if contains informed type, otherwise false</returns>
        bool Contains<ISearchItem>();

        /// <summary>
        /// Check if collection contains informed type
        /// </summary>
        /// <param name="searchItemType">Type to check</param>
        /// <returns>True if contains informed type, otherwise false</returns>
        bool Contains(Type searchItemType);

        /// <summary>
        /// Add items to collection
        /// </summary>
        /// <param name="item">Item to add in collection</param>
        /// <returns>Itself</returns>
        void Add(ISearchItem item);

        /// <summary>
        /// Execute items and get query instructions
        /// </summary>
        /// <param name="requestHandler">Handler to use in SOLR request</param>
        /// <returns>Query instructions</returns>
        JsonReader Execute(string requestHandler);
    }
}
