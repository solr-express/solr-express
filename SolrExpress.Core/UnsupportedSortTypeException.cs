using System;

namespace SolrExpress.Core
{
    public sealed class UnsupportedSortTypeException : Exception
    {
        public UnsupportedSortTypeException() :
            base(Resource.UnsupportedSortTypeException)
        {
        }
    }
}
