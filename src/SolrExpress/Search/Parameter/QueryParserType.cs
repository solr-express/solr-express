namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Query parser's type of SOLR's search
    /// </summary>
    public enum QueryParserType
    {
        /// <summary>
        /// Use "lucene" parser to process the main query parameter (q) in the request
        /// </summary>
        Lucene,

        /// <summary>
        /// Use "dismax" parser to process the main query parameter (q) in the request
        /// </summary>
        Dismax,

        /// <summary>
        /// Use "edismax" parser to process the main query parameter (q) in the request
        /// </summary>
        Edismax
    }
}
