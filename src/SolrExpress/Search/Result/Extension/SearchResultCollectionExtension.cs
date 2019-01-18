using System.Collections.Generic;
using System.Linq;

namespace SolrExpress.Search.Result.Extension
{
    public static class SearchResultCollectionExtension
    {
        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static IList<ISearchResult<TDocument>> Document<TDocument>(this IList<ISearchResult<TDocument>> searchResults, out IEnumerable<TDocument> data)
            where TDocument : Document
        {
            var result = searchResults.FirstOrDefault(q => q is IDocumentResult<TDocument>);

            data = ((IDocumentResult<TDocument>)result)?.Data ?? null;

            return searchResults;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static IList<ISearchResult<TDocument>> Facets<TDocument>(this IList<ISearchResult<TDocument>> searchResults, out IEnumerable<IFacetItem> data)
            where TDocument : Document
        {
            var result = searchResults.FirstOrDefault(q => q is IFacetsResult<TDocument>);

            data = ((IFacetsResult<TDocument>)result)?.Data ?? null;

            return searchResults;
        }

        /// <summary>
        /// Returns informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static IList<ISearchResult<TDocument>> Information<TDocument>(this IList<ISearchResult<TDocument>> searchResults, out Information data)
            where TDocument : Document
        {
            var result = searchResults.FirstOrDefault(q => q is IInformationResult<TDocument>);

            data = ((IInformationResult<TDocument>)result)?.Data ?? null;

            return searchResults;
        }
    }
}
