using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;
using System.Collections.Generic;

namespace SolrExpress.Core.Extension
{
    public static class SolrQueryResultExtension
    {
        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static SolrQueryResult<TDocument> Document<TDocument>(this SolrQueryResult<TDocument> result, out List<TDocument> data)
            where TDocument : IDocument
        {
            data = result.Get(result.BuilderFactory.GetDocumentBuilder()).Data;

            return result;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param
        public static SolrQueryResult<TDocument> FacetField<TDocument>(this SolrQueryResult<TDocument> result, out List<FacetKeyValue<string>> data)
            where TDocument : IDocument
        {
            data = result.Get(result.BuilderFactory.GetFacetFieldBuilder()).Data;

            return result;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        public static SolrQueryResult<TDocument> FacetQuery<TDocument>(this SolrQueryResult<TDocument> result, out Dictionary<string, long> data)
            where TDocument : IDocument
        {
            data = result.Get(result.BuilderFactory.GetFacetQueryBuilder()).Data;

            return result;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="data">Facet range list</param>
        public static SolrQueryResult<TDocument> FacetRange<TDocument>(this SolrQueryResult<TDocument> result, out List<FacetKeyValue<FacetRange>> data)
            where TDocument : IDocument
        {
            data = result.Get(result.BuilderFactory.GetFacetRangeBuilder()).Data;

            return result;
        }

        /// <summary>
        /// Returns statistics about the search
        /// </summary>
        /// <param name="data">Statics about search execution</param>
        public static SolrQueryResult<TDocument> Statistic<TDocument>(this SolrQueryResult<TDocument> result, out Statistic data)
            where TDocument : IDocument
        {
            data = result.Get(result.BuilderFactory.GetStatisticBuilder()).Data;

            return result;
        }
    }
}
