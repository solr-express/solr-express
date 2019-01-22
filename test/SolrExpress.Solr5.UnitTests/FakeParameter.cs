using SolrExpress.Search.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests
{
    public class FakeParameter : ISearchParameter
    {
        public bool Equals(ISearchParameter other)
        {
            throw new NotImplementedException();
        }
    }
}
