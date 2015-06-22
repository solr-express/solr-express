using System;
using System.Collections.Generic;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Linq
{
    /// <summary>
    /// SOLR query result extensions methods used to create facilities in the use of the classes
    /// </summary>
    public static class SolrQueryResultExtension
    {
        /// <summary>
        /// Returns a document list
        /// </summary>
        /// <param name="solrQueryResult">The solr query result</param>
        /// <param name="data">Documents list</param>
        public static SolrQueryResult Document<TDocument>(this SolrQueryResult solrQueryResult, out List<TDocument> data)
            where TDocument : IDocument
        {
            data = solrQueryResult.Get(new Solr5.Builder.DocumentBuilder<TDocument>()).Data;

            return solrQueryResult;
        }

        /// <summary>
        /// Returns a facet field list
        /// </summary>
        /// <param name="solrQueryResult">The solr query result</param>
        /// <param name="data">Facet field list</param>
        public static SolrQueryResult FacetField(this SolrQueryResult solrQueryResult, out List<FacetKeyValue<string>> data)
        {
            data = solrQueryResult.Get(new Solr5.Builder.FacetFieldResultBuilder()).Data;

            return solrQueryResult;
        }

        /// <summary>
        /// Returns a facet query list
        /// </summary>
        /// <param name="solrQueryResult">The solr query result</param>
        /// <param name="data">Facet query list</param>
        public static SolrQueryResult FacetQuery(this SolrQueryResult solrQueryResult, out Dictionary<string, long> data)
        {
            data = solrQueryResult.Get(new Solr5.Builder.FacetQueryResultBuilder()).Data;

            return solrQueryResult;
        }

        /// <summary>
        /// Returns a facet range list
        /// </summary>
        /// <param name="solrQueryResult">The solr query</param>
        /// <param name="data">Facet range list</param>
        public static SolrQueryResult FacetRange(this SolrQueryResult solrQueryResult, out List<FacetKeyValue<FacetRange>> data)
        {
            data = solrQueryResult.Get(new Solr5.Builder.FacetRangeResultBuilder()).Data;

            return solrQueryResult;
        }

        /// <summary>
        /// Returns statistics about the search
        /// </summary>
        /// <param name="solrQueryResult">The solr query</param>
        /// <param name="isEmpty">True if search result return empty result, false otherwise</param>
        /// <param name="documentCount">Total quantity of documents in the result</param>
        /// <param name="timeToExecution">Time to SOLR process the requested search</param>
        public static SolrQueryResult Statistic(this SolrQueryResult solrQueryResult, out bool isEmpty, out long documentCount, out TimeSpan timeToExecution)
        {
            var builder = solrQueryResult.Get(new Solr5.Builder.StatisticResultBuilder());
            isEmpty = builder.IsEmpty;
            documentCount = builder.DocumentCount;
            timeToExecution = builder.TimeToExecution;

            return solrQueryResult;
        }
    }
}
