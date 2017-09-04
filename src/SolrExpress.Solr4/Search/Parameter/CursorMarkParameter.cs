using SolrExpress.Search.Parameter;
using System;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class CursorMarkParameter : ICursorMarkParameter
    {
        public string CursorMark { get => throw new UnsupportedFeatureException(); set => throw new UnsupportedFeatureException(); }
    }
}
