using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FacetQueryParameterTests
    {
        [TestMethod]
        public void WhenExecuteWithDefaultArguments_CreateJson()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""query"": {
                    ""q"": ""avg('Y')""
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetQueryParameter("X", "avg('Y')");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        [TestMethod]
        public void WhenExecuteWithSortTypeAndDirection_CreateJson()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""query"": {
                    ""q"": ""avg('Y')"",
                    ""sort"": {
                      ""count"": ""desc""
                    }
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetQueryParameter("X", "avg('Y')", SolrFacetSortType.Quantity, false);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

    }
}
