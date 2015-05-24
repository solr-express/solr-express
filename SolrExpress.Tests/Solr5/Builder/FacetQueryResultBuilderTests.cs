using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Builder;

namespace SolrExpress.Tests.Solr5.Builder
{
    [TestClass]
    public class FacetQueryResultBuilderTests
    {
        [TestMethod]
        public void WhenExecute_CreateAListWithFacetsParsedInConcretClasses()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetQuery"": {
                      ""count"": 10
                    }
                }
            }");

            var parameter = new FacetQueryResultBuilder();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.IsTrue(parameter.Data.ContainsKey("facetQuery"));
            Assert.AreEqual(10, parameter.Data["facetQuery"]);
        }
    }
}
