namespace SolrExpress.Search.Parameter
{
    /// <summary>
    /// Default handlers to use in SOLR request
    /// </summary>
    public static class RequestHandler
    {
        public static string Select
        {
            get { return "select"; }
        }

        public static string Get
        {
            get { return "get"; }
        }

        public static string Query
        {
            get { return "query"; }
        }

        public static string Update
        {
            get { return "update"; }
        }

        public static string Stream
        {
            get { return "stream"; }
        }
    }
}
