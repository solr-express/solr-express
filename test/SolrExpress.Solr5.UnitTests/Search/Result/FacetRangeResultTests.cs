using Moq;
using Newtonsoft.Json.Linq;
using SolrExpress.Core.Search;
using SolrExpress.Core.Search.Result;
using SolrExpress.Core.Utility;
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

            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder).Configure("facetRange", q=> q.PropInteger, "10", "10", "100")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>(expressionBuilder);

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocumentWithAnyPropertyTypes>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count());
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

            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder).Configure("facetRange", q=> q.PropDateTime, "+10DAYS", "NOW/YEAR-1", "NOW/DAY+1")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>(expressionBuilder);

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocumentWithAnyPropertyTypes>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count());
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
            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);
            
            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder).Configure("facetRange", q=> q.PropDecimal, "10", "10", "100")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>(expressionBuilder);

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocumentWithAnyPropertyTypes>)result).Data.ToList();
            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count());
            Assert.IsType(typeof(FacetRange<decimal>), data[0].Data.First().Key);
        }

        /// <summary>
        /// Where   Using a FacetRangeResult instance
        /// When    Invoking the method "Execute" using a valid JSON (with date time values and gaps)
        /// What    Parse to informed concret classes
        /// </summary>
        [Fact]
        public void FacetRangeResult004()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetRange"": {
                          ""buckets"": [
                            {
                              ""val"": ""2016-01-01T00:00:00.000Z"",
                              ""count"": 10
                            },
                            {
                              ""val"": ""2018-01-01T00:00:00.000Z"",
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

            var expressionCache = new ExpressionCache<TestDocumentWithAnyPropertyTypes>();
            var expressionBuilder = (IExpressionBuilder<TestDocumentWithAnyPropertyTypes>)new ExpressionBuilder<TestDocumentWithAnyPropertyTypes>(expressionCache);

            var parameters = new List<ISearchParameter> {
                new FacetRangeParameter<TestDocumentWithAnyPropertyTypes>(expressionBuilder).Configure("facetRange", q=> q.PropDateTime, "+10DAYS", "NOW/YEAR-1", "NOW/DAY+1")
            };
            var result = (IConvertJsonObject)new FacetRangeResult<TestDocumentWithAnyPropertyTypes>(expressionBuilder);

            // Act
            result.Execute(parameters, jObject);

            // Assert
            var data = ((IFacetRangeResult<TestDocumentWithAnyPropertyTypes>)result).Data.ToList();
            var element0 = data[0].Data.ElementAt(0);
            var element1 = data[0].Data.ElementAt(1);
            var element2 = data[0].Data.ElementAt(2);
            var element3 = data[0].Data.ElementAt(3);

            Assert.Equal(1, data.Count);
            Assert.Equal("facetRange", data[0].Name);
            Assert.Equal(4, data[0].Data.Count);
            Assert.IsType(typeof(FacetRange<DateTime>), element0.Key);

            Assert.Null(element0.Key.GetMinimumValue());
            Assert.Equal(DateTime.Now.Date.AddYears(-1), element0.Key.GetMaximumValue());
            Assert.Equal(30, element0.Value);

            Assert.Equal(DateTime.Parse("2016-01-01T00:00:00.000Z"), element1.Key.GetMinimumValue());
            Assert.Equal(DateTime.Parse("2016-01-01T00:00:00.000Z").AddDays(10), element1.Key.GetMaximumValue());
            Assert.Equal(10, element1.Value);

            Assert.Equal(DateTime.Parse("2018-01-01T00:00:00.000Z"), element2.Key.GetMinimumValue());
            Assert.Equal(DateTime.Parse("2018-01-01T00:00:00.000Z").AddDays(10), element2.Key.GetMaximumValue());
            Assert.Equal(20, element2.Value);

            Assert.Null(element3.Key.GetMaximumValue());
            Assert.Equal(DateTime.Now.Date.AddDays(1), element3.Key.GetMinimumValue());
            Assert.Equal(40, element3.Value);
        }
    }
}
