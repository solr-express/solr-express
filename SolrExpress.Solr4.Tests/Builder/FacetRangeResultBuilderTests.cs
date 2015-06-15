using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr4.Builder;

namespace SolrExpress.Solr4.Tests.Builder
{
    [TestClass]
    public class FacetRangeResultBuilderTests
    {
        /// <summary>
        /// Where   Using a FacetRangeResultBuilder instance
        /// When    Invoking the method "Execute" using a valid JSON
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResultBuilder001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                  ""facet_counts"":{
                    ""facet_ranges"":{
                      ""facetRange"":{
                        ""counts"":[
                          ""10.0"",2,
                          ""20.0"",0,
                          ""30.0"",0,
                          ""40.0"",0,
                          ""50.0"",0,
                          ""60.0"",0,
                          ""70.0"",1,
                          ""80.0"",0,
                          ""90.0"",1],
                        ""gap"":10.0,
                        ""start"":10.0,
                        ""end"":100.0,
                        ""before"":3,
                        ""after"":9}}}
            }");

            var parameter = new FacetRangeResultBuilder();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(11, parameter.Data[0].Data.Count);
        }
    }
}
