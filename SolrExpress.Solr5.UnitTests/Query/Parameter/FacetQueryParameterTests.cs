using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Core.Query.ParameterValue;
using SolrExpress.Solr5.Query.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Query.Parameter
{
    [TestClass]
    public class FacetQueryParameterTests
    {
        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter001()
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
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new Any("avg('Y')"));

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter002()
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
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new Any("avg('Y')"), FacetSortType.CountDesc);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Create the instance with null in alias name
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetQueryParameter003()
        {
            // Arrange / Act / Assert
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure(null, new Any("x"));
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Create the instance with null in expression
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetQueryParameter004()
        {
            // Arrange / Act / Assert
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("x", null);
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetQueryParameter005()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""X"": {
                  ""query"": {
                    ""q"": ""{!ex=tag1,tag2}avg('Y')""
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetQueryParameter<TestDocument>();
            parameter.Configure("X", new Any("avg('Y')"), excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
