using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Builder;

namespace SolrExpress.Tests.Solr5.Builder
{
    [TestClass]
    public class FacetFieldResultBuilderTests
    {
        [TestMethod]
        public void WhenExecute_CreateAListWithFacetsParsedInConcretClasses()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetField"": {
                        ""buckets"": [
                        {
                            ""val"": ""VALUE001"",
                            ""count"": 10
                        },
                        {
                            ""val"": ""VALUE002"",
                            ""count"": 20
                        }]
                    }
                }
            }");

            var parameter = new FacetFieldResultBuilder();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetField", parameter.Data[0].Name);
            Assert.AreEqual(2, parameter.Data[0].Data.Count);
            Assert.IsTrue(parameter.Data[0].Data.ContainsKey("VALUE001"));
            Assert.AreEqual(10, parameter.Data[0].Data["VALUE001"]);
            Assert.IsTrue(parameter.Data[0].Data.ContainsKey("VALUE002"));
            Assert.AreEqual(20, parameter.Data[0].Data["VALUE002"]);
        }
    }
}
