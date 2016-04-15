using SolrExpress.Core.Parameter;
using System;

namespace SolrExpress.Core.Extension.Internal
{
    /// <summary>
    /// Extension class used in Expression<Func<TDocument>> to manipulate SolrFacetSortType
    /// </summary>
    public static class SolrFacetSortTypeExtension
    {
        /// <summary>
        /// Get the sort type and direction
        /// </summary>
        /// <param name="solrFacetSortType">Type used in match</param>
        /// <param name="typeName">Type name</param>
        /// <param name="sortName">Sort direction</param>
        internal static void GetSolrFacetSort(this SolrFacetSortType solrFacetSortType, out string typeName, out string sortName)
        {
            switch (solrFacetSortType)
            {
                case SolrFacetSortType.IndexAsc:
                    typeName = "index";
                    sortName = "asc";
                    break;
                case SolrFacetSortType.IndexDesc:
                    typeName = "index";
                    sortName = "desc";
                    break;
                case SolrFacetSortType.CountAsc:
                    typeName = "count";
                    sortName = "asc";
                    break;
                case SolrFacetSortType.CountDesc:
                    typeName = "count";
                    sortName = "desc";
                    break;
                default:
                    throw new ArgumentException(nameof(solrFacetSortType));
            }
        }
    }
}
