using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Enumerator;
using SolrExpress.Solr5.Parameter;
using SolrExpress.Solr5.Tests;

namespace SolrExpress.Tests.Solr5.Parameter
{
    [TestClass]
    public class FacetRangeParameterTests
    {
        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""Id"",
                    ""gap"": ""1"",
                    ""start"": ""10"",
                    ""end"": ""20"",
                    ""other"": [
                      ""before"",
                      ""after""
                    ]
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>(q => q.Id, "X", "1", "10", "20");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetRangeParameter002()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""range"": {
                    ""field"": ""Id"",
                    ""gap"": ""1"",
                    ""start"": ""10"",
                    ""end"": ""20"",
                    ""other"": [
                      ""before"",
                      ""after""
                    ],
                    ""sort"": {
                      ""count"": ""desc""
                    }
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetRangeParameter<TestDocument>(q => q.Id, "X", "1", "10", "20", SolrFacetSortType.QuantityDesc);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
