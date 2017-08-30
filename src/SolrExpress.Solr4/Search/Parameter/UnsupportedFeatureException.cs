using System;

namespace SolrExpress.Solr4.Search.Parameter
{
    public sealed class UnsupportedFeatureException : Exception
    {
        public UnsupportedFeatureException() : base(Resource.UnsupportedFeatureException)
        {
        }
    }
}
