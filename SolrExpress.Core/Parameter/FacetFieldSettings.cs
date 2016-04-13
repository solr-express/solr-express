using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SolrExpress.Core.Parameter
{
    public class FacetFieldSettings<TDocument> : ISettings
        where TDocument : IDocument
    {
        /// <summary>
        /// Expression used to find the property name
        /// </summary>
        public Expression<Func<TDocument, object>> Expression { get; set; }

        /// <summary>
        /// Sort type of the result of the facet
        /// </summary>
        public SolrFacetSortType? SortType { get; set; }

        /// <summary>
        /// Limit of itens in facet's result
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// List of tags to exclude in facet calculation
        /// </summary>
        public List<string> Excludes { get; set; }
    }
}
