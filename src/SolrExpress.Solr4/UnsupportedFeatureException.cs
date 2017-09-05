using System;

namespace SolrExpress.Solr4
{
    public sealed class UnsupportedFeatureException : Exception
    {
        public UnsupportedFeatureException() : base(Resource.UnsupportedFeatureException)
        {
        }
    }
}
