using SolrExpress.Search.Result;
using System.Collections.Generic;

namespace SolrExpress.Extension
{
    public static class SearchResultBuilderExtension
    {
        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static SearchResultBuilder<TDocument> Document<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IEnumerable<TDocument> data)
            where TDocument : IDocument
        {
            //TODO: DI
            IDocumentResult<TDocument> result = null;

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static SearchResultBuilder<TDocument> FacetField<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IEnumerable<FacetKeyValue<string>> data)
            where TDocument : IDocument
        {
            //TODO: DI
            IFacetFieldResult<TDocument> result = null;

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        public static SearchResultBuilder<TDocument> FacetQuery<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IDictionary<string, long> data)
            where TDocument : IDocument
        {
            //TODO: DI
            IFacetQueryResult<TDocument> result = null;

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="data">Facet range list</param>
        public static SearchResultBuilder<TDocument> FacetRange<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IEnumerable<FacetKeyValue<FacetRange>> data)
            where TDocument : IDocument
        {
            //TODO: DI
            IFacetRangeResult<TDocument> result = null;

            data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static SearchResultBuilder<TDocument> Information<TDocument>(this SearchResultBuilder<TDocument> searchResult, out Information data)
            where TDocument : IDocument
        {
            //TODO: DI
            IInformationResult<TDocument> result = null;

            data = searchResult.Get(result).Data;

            return searchResult;
        }

    }
}
