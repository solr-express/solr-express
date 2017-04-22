using System.Collections.Generic;

namespace SolrExpress.Search.Result.Extension
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
            var result = searchResult.ServiceProvider.GetService<IDocumentResult<TDocument>>();
            searchResult.Add(result);

            data = null;
            //data = searchResult.Get(result).Data;

            return searchResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static SearchResultBuilder<TDocument> Facets<TDocument>(this SearchResultBuilder<TDocument> searchResult, out IEnumerable<FacetKeyValue> data)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IFacetsResult<TDocument>>();
            searchResult.Add(result);

            // TODO: Review
            data = null;
            //data = searchResult.Get(result).Data;

            return searchResult;
        }
        
        /// <summary>
        /// Returns informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static SearchResultBuilder<TDocument> Information<TDocument>(this SearchResultBuilder<TDocument> searchResult, out Information data)
            where TDocument : IDocument
        {
            var result = searchResult.ServiceProvider.GetService<IInformationResult<TDocument>>();
            searchResult.Add(result);

            // TODO: Review
            data = null;
            //data = searchResult.Get(result).Data;

            return searchResult;
        }

    }
}
