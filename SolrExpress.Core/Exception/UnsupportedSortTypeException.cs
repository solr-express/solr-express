namespace SolrExpress.Core.Exception
{
    public sealed class UnsupportedSortTypeException : System.Exception
    {
        public UnsupportedSortTypeException() :
            base("Descending sort type is an unsupported feature in Solr 4")
        {
        }
    }
}
