using SolrExpress.Search.Parameter;

namespace SolrExpress.Benchmarks.Helper
{
    public class FakeAnyParameter : IAnyParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public bool Equals(ISearchParameter other)
        {
            throw new System.NotImplementedException();
        }
    }
}
