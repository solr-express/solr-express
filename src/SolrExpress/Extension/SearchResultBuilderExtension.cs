using SolrExpress.Search.Result;

namespace SolrExpress.Extension
{
    public static class SearchResultBuilderExtension
    {
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
