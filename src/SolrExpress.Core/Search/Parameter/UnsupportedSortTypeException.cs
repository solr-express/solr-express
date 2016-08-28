using System;

namespace SolrExpress.Core.Search.Parameter
{
    public sealed class UnsupportedSortTypeException : Exception
    {
        public UnsupportedSortTypeException() :
            base(Resource.UnsupportedSortTypeException)
        {
        }
    }
}
