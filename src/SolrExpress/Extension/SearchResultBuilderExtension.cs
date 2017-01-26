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
