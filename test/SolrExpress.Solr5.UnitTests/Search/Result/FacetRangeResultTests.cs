using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Solr5.Search.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Result
{
    public class FacetRangeResultTests
    {
        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with integer values)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
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

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>().Configure("facetRange", q=> q.PropInteger, "10", "10", "100")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocument>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<int>), data[0].Data.First().Key);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with date time values)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
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

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>().Configure("facetRange", q=> q.PropDateTime, "+10DAYS", "NOW/YEAR-1", "NOW/DAY+1")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocument>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<DateTime>), data[0].Data.First().Key);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with float values)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
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

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>().Configure("facetRange", q=> q.PropDecimal, "10", "10", "100")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>();

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocument>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<decimal>), data[0].Data.First().Key);
        }
    }
}
