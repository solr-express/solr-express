﻿using System.Collections.Generic;

namespace SolrExpress.Core.Search.Result
{
    /// <summary>
    /// Represents a facet value
    /// </summary>
    /// <typeparam name="TKey">Value of the facet</typeparam>
    public sealed class FacetKeyValue<TKey>
    {
        /// <summary>
        /// Name of the facet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data list of the facet
        /// </summary>
        public Dictionary<TKey, long> Data { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { get; set; }
    }
}
