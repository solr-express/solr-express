using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query.Parameter;
using SolrExpress.Solr5.Parameter;
using System;

namespace SolrExpress.Solr5.UnitTests.Parameter
{
    [TestClass]
    public class FacetFieldParameterTests
    {
        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter001()
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
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the sort type and direction parameters
        /// What    Create a valid JSON
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter002()
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
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, SolrFacetSortType.CountDesc);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FacetFieldParameter003()
        {
            // Arrange / Act / Assert
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(null);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the limit parameter
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter004()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""Id"",
                    ""limit"": 10
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, limit: 10);

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking the method "Execute" using the default arguments and an excluding list
        /// What    Create a valid string
        /// </summary>
        [TestMethod]
        public void FacetFieldParameter005()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              ""facet"": {
                ""Id"": {
                  ""terms"": {
                    ""field"": ""{!ex=tag1,tag2}Id""
                  }
                }
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new FacetFieldParameter<TestDocument>();
            parameter.Configure(q => q.Id, excludes: new[] { "tag1", "tag2" });

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}
