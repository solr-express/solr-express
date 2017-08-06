namespace SolrExpress.Search.Result.Extension
{
    public static class SearchResultBuilderExtension
    {
        /// <summary>
        /// Add logic to process result and get document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static SearchResultBuilder<TDocument> Document<TDocument>(this SearchResultBuilder<TDocument> searchResult)
            where TDocument : Document
        {
            var result = searchResult.ServiceProvider.GetService<IDocumentResult<TDocument>>();
            searchResult.Add(result);

            return searchResult;
        }

        /// <summary>
        /// Add logic to process result and get facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static SearchResultBuilder<TDocument> Facets<TDocument>(this SearchResultBuilder<TDocument> searchResult)
            where TDocument : Document
        {
            var result = searchResult.ServiceProvider.GetService<IFacetsResult<TDocument>>();
            searchResult.Add(result);

            return searchResult;
        }

        /// <summary>
        /// Add logic to process result and get informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static SearchResultBuilder<TDocument> Information<TDocument>(this SearchResultBuilder<TDocument> searchResult)
            where TDocument : Document
        {
            var result = searchResult.ServiceProvider.GetService<IInformationResult<TDocument>>();
            searchResult.Add(result);

            return searchResult;
        }
    }
}
