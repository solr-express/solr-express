using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Search.Parameter
{
    public class CursorMarkParameter : ICursorMarkParameter, ISearchItemExecution<List<string>>
    {
        public string CursorMark { get => throw new UnsupportedFeatureException(); set => throw new UnsupportedFeatureException(); }

        public void AddResultInContainer(List<string> container)
        {
            throw new UnsupportedFeatureException();
        }

        public void Execute()
        {
            throw new UnsupportedFeatureException();
        }
    }
}
