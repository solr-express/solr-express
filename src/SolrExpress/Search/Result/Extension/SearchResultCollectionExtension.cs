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
        public static SearchResultCollection<TDocument> GetDocument<TDocument>(this SearchResultCollection<TDocument> searchResultCollection, out IEnumerable<TDocument> data)
            where TDocument : Document
        {
            var result = searchResultCollection.GetList().First(q => q is IDocumentResult<TDocument>);

            data = ((IDocumentResult<TDocument>)result).Data;

            return searchResultCollection;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param>
        public static SearchResultCollection<TDocument> GetFacets<TDocument>(this SearchResultCollection<TDocument> searchResultCollection, out IEnumerable<IFacetItem> data)
            where TDocument : Document
        {
            var result = searchResultCollection.GetList().First(q => q is IFacetsResult<TDocument>);

            data = ((IFacetsResult<TDocument>)result).Data;

            return searchResultCollection;
        }

        /// <summary>
        /// Returns informations about the search
        /// </summary>
        /// <param name="data">Informations about search execution</param>
        public static SearchResultCollection<TDocument> GetInformation<TDocument>(this SearchResultCollection<TDocument> searchResultCollection, out Information data)
            where TDocument : Document
        {
            var result = searchResultCollection.GetList().First(q => q is IInformationResult<TDocument>);

            data = ((IInformationResult<TDocument>)result).Data;

            return searchResultCollection;
        }
    }
}
