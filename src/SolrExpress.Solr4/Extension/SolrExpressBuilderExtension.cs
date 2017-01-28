namespace SolrExpress.Solr4.Extension
{
    public static class SolrExpressBuilderExtension
    {
        /// <summary>
        /// Use SolrExpress services implemented in SOLR 4
        /// </summary>
        /// <param name="solrExpressBuilder">Builder to control SolrExpress behavior</param>
        /// <returns>Builder to control SolrExpress behavior</returns>
        public static SolrExpressBuilder<TDocument> UseSolr5<TDocument>(this SolrExpressBuilder<TDocument> solrExpressBuilder)
            where TDocument : IDocument
        {
            return solrExpressBuilder;
        }
    }
}
