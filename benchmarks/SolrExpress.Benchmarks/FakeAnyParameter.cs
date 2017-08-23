using SolrExpress.Search.Parameter;

namespace SolrExpress.Benchmarks
{
    public class FakeAnyParameter : IAnyParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
