namespace SolrExpress.Core.Exception
{
    public sealed class UnsupportedSortTypeException : System.Exception
    {
        public UnsupportedSortTypeException() :
            base(Resource.UnsupportedSortTypeException)
        {
        }
    }
}
