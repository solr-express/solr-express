using SolrExpress.Core.DependencyInjection;
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
        public static ISearchResult<TDocument> Document<TDocument>(this ISearchResult<TDocument> queryResult, out IEnumerable<TDocument> data)
            where TDocument : IDocument
        {
            var result = ApplicationServices
                .Current
                .GetService<IDocumentResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static ISearchResult<TDocument> FacetField<TDocument>(this ISearchResult<TDocument> queryResult, out IEnumerable<FacetKeyValue<string>> data)
            where TDocument : IDocument
        {
            var result = ApplicationServices
                .Current
                .GetService<IFacetFieldResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        public static ISearchResult<TDocument> FacetQuery<TDocument>(this ISearchResult<TDocument> queryResult, out IDictionary<string, long> data)
            where TDocument : IDocument
        {
            var result = ApplicationServices
                .Current
                .GetService<IFacetQueryResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="data">Facet range list</param>
        public static ISearchResult<TDocument> FacetRange<TDocument>(this ISearchResult<TDocument> queryResult, out IEnumerable<FacetKeyValue<FacetRange>> data)
            where TDocument : IDocument
        {
            var result = ApplicationServices
                .Current
                .GetService<IFacetRangeResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns statistics about the search
        /// </summary>
        /// <param name="data">Statics about search execution</param>
        public static ISearchResult<TDocument> Information<TDocument>(this ISearchResult<TDocument> queryResult, out Information data)
            where TDocument : IDocument
        {
            var result = ApplicationServices
                .Current
                .GetService<IInformationResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }
    }
}
