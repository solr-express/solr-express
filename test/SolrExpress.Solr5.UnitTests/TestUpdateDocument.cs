namespace SolrExpress.Solr5.UnitTests
{
    public class TestUpdateDocument : Document
    {
        [SolrField("dummy")]
        public string Dummy { get; set; }

        [SolrField("dummy2")]
        public string Dummy2 { get; set; }

        [SolrField("dummy3")]
        public string Dummy3 { get; set; }

        [SolrField("number_value")]
        public int NumberValue { get; set; }

        [SolrField("number_value2")]
        public int NumberValue2 { get; set; }

        [SolrField("array_value")]
        public int[] ArrayValue { get; set; }

        [SolrField("array_value2")]
        public int[] ArrayValue2 { get; set; }
    }
}
