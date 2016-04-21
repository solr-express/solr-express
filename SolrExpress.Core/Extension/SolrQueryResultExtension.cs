using SolrExpress.Core.Query;
using SolrExpress.Core.Result;
using System.Collections.Generic;

namespace SolrExpress.Core.Extension
{
    public static class SolrQueryResultExtension
    {
        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="data">Documents list</param>
        public static SolrQueryResult<TDocument> Document<TDocument>(this SolrQueryResult<TDocument> queryResult, out List<TDocument> data)
            where TDocument : IDocument
        {
            var result = queryResult.Resolver.GetInstance<IDocumentResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="data">Facet field list</param
        public static SolrQueryResult<TDocument> FacetField<TDocument>(this SolrQueryResult<TDocument> queryResult, out List<FacetKeyValue<string>> data)
            where TDocument : IDocument
        {
            var result = queryResult.Resolver.GetInstance<IFacetFieldResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="data">Facet query list</param>
        public static SolrQueryResult<TDocument> FacetQuery<TDocument>(this SolrQueryResult<TDocument> queryResult, out Dictionary<string, long> data)
            where TDocument : IDocument
        {
            var result = queryResult.Resolver.GetInstance<IFacetQueryResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="data">Facet range list</param>
        public static SolrQueryResult<TDocument> FacetRange<TDocument>(this SolrQueryResult<TDocument> queryResult, out List<FacetKeyValue<FacetRange>> data)
            where TDocument : IDocument
        {
            var result = queryResult.Resolver.GetInstance<IFacetRangeResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }

        /// <summary>
        /// Returns statistics about the search
        /// </summary>
        /// <param name="data">Statics about search execution</param>
        public static SolrQueryResult<TDocument> Statistic<TDocument>(this SolrQueryResult<TDocument> queryResult, out Statistic data)
            where TDocument : IDocument
        {
            var result = queryResult.Resolver.GetInstance<IStatisticResult<TDocument>>();

            data = queryResult.Get(result).Data;

            return queryResult;
        }
    }
}
