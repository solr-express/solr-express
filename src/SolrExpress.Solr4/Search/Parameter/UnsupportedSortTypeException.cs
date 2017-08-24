using System;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class UnsupportedSortTypeException : Exception
    {
        public UnsupportedSortTypeException() : base(Resource.UnsupportedSortTypeException)
        {
        }
    }
}
