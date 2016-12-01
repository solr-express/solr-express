using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using System.Collections.Generic;

namespace SolrExpress.Core.Extension
{
    public static class ISearchResultExtensions
    {
        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static ISearchResult<TDocument> Document<TDocument>(this ISearchResult<TDocument> searchResult, out IEnumerable<TDocument> data)
            where TDocument : IDocument
        {
            var result = searchResult
                .Engine
                .GetService<IDocumentResult<TDocument>>();

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static ISearchResult<TDocument> FacetField<TDocument>(this ISearchResult<TDocument> searchResult, out IEnumerable<FacetKeyValue<string>> data)
            where TDocument : IDocument
        {
            var result = searchResult
                .Engine
                .GetService<IFacetFieldResult<TDocument>>();

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        public static ISearchResult<TDocument> FacetQuery<TDocument>(this ISearchResult<TDocument> searchResult, out IDictionary<string, long> data)
            where TDocument : IDocument
        {
            var result = searchResult
                .Engine
                .GetService<IFacetQueryResult<TDocument>>();

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        /// <param name="tag">Facet tag</param>
        public static ISearchResult<TDocument> FacetQuery<TDocument>(this ISearchResult<TDocument> searchResult, out IDictionary<string, long> data, out object tag)
            where TDocument : IDocument
        {
            var result = searchResult
                .Engine
                .GetService<IFacetQueryResult<TDocument>>();

            var facetQueryResult = searchResult.Get(result);
            data = facetQueryResult.Data;
            tag = facetQueryResult.Tag;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="data">Facet range list</param>
        public static ISearchResult<TDocument> FacetRange<TDocument>(this ISearchResult<TDocument> searchResult, out IEnumerable<FacetKeyValue<FacetRange>> data)
            where TDocument : IDocument
        {
            var result = searchResult
                .Engine
                .GetService<IFacetRangeResult<TDocument>>();

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns statistics about the search
        /// </summary>
        /// <param name="data">Statics about search execution</param>
        public static ISearchResult<TDocument> Information<TDocument>(this ISearchResult<TDocument> searchResult, out Information data)
            where TDocument : IDocument
        {
            var result = searchResult
                .Engine
                .GetService<IInformationResult<TDocument>>();

            data = searchResult.Get(result).Data;

            return searchResult;
        }
    }
}
