namespace SolrExpress.UnitTests
{
    public class TestDocumentDynamic : Document
    {
        [SolrField("no_dynamic")]
        public string NoDynamic { get; set; }

        [SolrField("dynamic", IsDynamicField = true, DynamicFieldPrefixName = "prefix", DynamicFieldSuffixName = "suffix")]
        public string DynamicWithPrefixAndSufix { get; set; }

        [SolrField("dynamic", IsDynamicField = true, DynamicFieldPrefixName = "prefix")]
        public string DynamicWithPrefix { get; set; }

        [SolrField("dynamic", IsDynamicField = true, DynamicFieldSuffixName = "suffix")]
        public string DynamicWithSuffix { get; set; }
    }
}
