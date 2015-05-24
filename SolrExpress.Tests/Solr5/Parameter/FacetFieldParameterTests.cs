using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Enumerator;
using SolrExpress.Solr5.Parameter;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FacetFieldParameterTests
    {
        [TestMethod]
        public void WhenExecuteWithDefaultArguments_CreateJson()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""Id""
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>(q => q.Id);

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
                ""Id"": {
                  ""terms"": {
                    ""field"": ""Id"",
                    ""sort"": {
                      ""count"": ""desc""
                    }
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>(q => q.Id, SolrFacetSortType.Quantity, false);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
