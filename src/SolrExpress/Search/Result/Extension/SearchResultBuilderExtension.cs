using System.Collections.Generic;

namespace SolrExpress.Search.Result.Extension
{
    public static class SearchResultBuilderExtension
    {
        /// <summary>
        /// Add logic to process result and get document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static SearchResultBuilder<TDocument> ProcessDocument<TDocument>(this SearchResultBuilder<TDocument> searchResult)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IDocumentResult<TDocument>>();
            searchResult.Add(result);

            return searchResult;
        }

        /// <summary>
        /// Add logic to process result and get facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static SearchResultBuilder<TDocument> ProcessFacets<TDocument>(this SearchResultBuilder<TDocument> searchResult)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IFacetsResult<TDocument>>();
            searchResult.Add(result);

            return searchResult;
        }

        /// <summary>
        /// Add logic to process result and get informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static SearchResultBuilder<TDocument> ProcessInformation<TDocument>(this SearchResultBuilder<TDocument> searchResult)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IInformationResult<TDocument>>();
            searchResult.Add(result);

            return searchResult;
        }

        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static SearchResultBuilder<TDocument> GetDocument<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IEnumerable<TDocument> data)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IDocumentResult<TDocument>>();

            data = result.Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static SearchResultBuilder<TDocument> GetFacets<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IEnumerable<FacetKeyValue> data)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IFacetsResult<TDocument>>();

            data = result.Data;

            return searchResult;
        }

        /// <summary>
        /// Returns informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static SearchResultBuilder<TDocument> GetInformation<TDocument>(this SearchResultBuilder<TDocument> searchResult, out Information data)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IInformationResult<TDocument>>();

            data = result.Data;

            return searchResult;
        }
    }
}
