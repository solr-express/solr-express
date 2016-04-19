using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Result;
using SolrExpress.Solr5.Result;
using System;
using System.Linq;

namespace SolrExpress.Solr5.UnitTests.Result
{
    [TestClass]
    public class FacetRangeResultTests
    {
        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with integer values)
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResult001()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetRange"": {
                          ""buckets"": [
                            {
                              ""val"": 100,
                              ""count"": 10
                            },
                            {
                              ""val"": 200,
                              ""count"": 20
                            }
                          ],
                          ""before"": {
                            ""count"": 30
                          },
                          ""after"": {
                            ""count"": 40
                          }
                        }
                }
            }");

            var parameter = new FacetRangeResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(4, parameter.Data[0].Data.Count);
            Assert.IsInstanceOfType(parameter.Data[0].Data.First().Key, typeof(FacetRange<int>));
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with date time values)
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResult002()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetRange"": {
                          ""buckets"": [
                            {
                              ""val"": ""2014-06-22T20:33:00.741Z"",
                              ""count"": 10
                            },
                            {
                              ""val"": ""2014-06-28T20:33:00.741Z"",
                              ""count"": 20
                            }
                          ],
                          ""before"": {
                            ""count"": 30
                          },
                          ""after"": {
                            ""count"": 40
                          }
                        }
                }
            }");
            
            var parameter = new FacetRangeResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(4, parameter.Data[0].Data.Count);
            Assert.IsInstanceOfType(parameter.Data[0].Data.First().Key, typeof(FacetRange<DateTime>));
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with float values)
        /// What    Parse to informed concret classes
        /// </summary>
        [TestMethod]
        public void FacetRangeResult003()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetRange"": {
                          ""buckets"": [
                            {
                              ""val"": 100.5,
                              ""count"": 10
                            },
                            {
                              ""val"": 200.5,
                              ""count"": 20
                            }
                          ],
                          ""before"": {
                            ""count"": 30
                          },
                          ""after"": {
                            ""count"": 40
                          }
                        }
                }
            }");

            var parameter = new FacetRangeResult<TestDocument>();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(4, parameter.Data[0].Data.Count);
            Assert.IsInstanceOfType(parameter.Data[0].Data.First().Key, typeof(FacetRange<float>));
        }
    }
}
