using Moq;
using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr5.Search.Parameter;
using SolrExpress.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FacetRangeParameterTests
    {
        public static IEnumerable<object[]> Data1
        {
            get
            {
                var solrOptions = new SolrExpressOptions();
                var solrConnection = new FakeSolrConnection<TestDocument>();
                var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
                expressionBuilder.LoadDocument();

                Action<IFacetRangeParameter<TestDocument>> config1 = facet =>
                {
                    facet.AliasName = "X";
                    facet.FieldExpression = q => q.Id;
                    facet.Start = "10";
                    facet.End = "20";
                    facet.Gap = "1";
                    facet.CountBefore = true;
                    facet.CountAfter = true;
                };
                var expected1 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""range"": {
                        ""field"": ""id"",
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

                Action<IFacetRangeParameter<TestDocument>> config2 = facet =>
                {
                    facet.AliasName = "X";
                    facet.FieldExpression = q => q.Id;
                    facet.Start = "10";
                    facet.End = "20";
                    facet.Gap = "1";
                    facet.CountBefore = true;
                    facet.CountAfter = true;
                    facet.SortType = FacetSortType.CountDesc;
                };
                var expected2 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""range"": {
                        ""field"": ""id"",
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

                Action<IFacetRangeParameter<TestDocument>> config3 = facet =>
                {
                    facet.AliasName = "X";
                    facet.FieldExpression = q => q.Id;
                    facet.Start = "10";
                    facet.End = "20";
                    facet.Gap = "1";
                    facet.CountBefore = true;
                    facet.CountAfter = true;
                    facet.Excludes = new[] { "tag1", "tag2" };
                };
                var expected3 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""range"": {
                        ""field"": ""id"",
                        ""domain"": {
                          ""excludeTags"": [
                            ""tag1"",
                            ""tag2""
                          ]
                        },
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

                Action<IFacetRangeParameter<TestDocument>> config4 = facet =>
                {
                    facet.AliasName = "X";
                    facet.FieldExpression = q => q.Id;
                    facet.Start = "10";
                    facet.End = "20";
                    facet.Gap = "1";
                    facet.CountBefore = false;
                    facet.CountAfter = true;
                    facet.Excludes = new[] { "tag1", "tag2" };
                };
                var expected4 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""range"": {
                        ""field"": ""id"",
                        ""domain"": {
                          ""excludeTags"": [
                            ""tag1"",
                            ""tag2""
                          ]
                        },
                        ""gap"": ""1"",
                        ""start"": ""10"",
                        ""end"": ""20"",
                        ""other"": [
                          ""after""
                        ]
                      }
                    }
                  }
                }");

                Action<IFacetRangeParameter<TestDocument>> config5 = facet =>
                {
                    facet.AliasName = "X";
                    facet.FieldExpression = q => q.Id;
                    facet.Start = "10";
                    facet.End = "20";
                    facet.Gap = "1";
                    facet.CountBefore = true;
                    facet.CountAfter = false;
                    facet.Excludes = new[] { "tag1", "tag2" };
                };
                var expected5 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""range"": {
                        ""field"": ""id"",
                        ""domain"": {
                          ""excludeTags"": [
                            ""tag1"",
                            ""tag2""
                          ]
                        },
                        ""gap"": ""1"",
                        ""start"": ""10"",
                        ""end"": ""20"",
                        ""other"": [
                          ""before""
                        ]
                      }
                    }
                  }
                }");

                Action<IFacetRangeParameter<TestDocument>> config6 = facet =>
                {
                    facet.AliasName = "X";
                    facet.FieldExpression = q => q.Id;
                    facet.Start = "10";
                    facet.End = "20";
                    facet.Gap = "1";
                    facet.Filter = new SearchQuery<TestDocument>(expressionBuilder).AddField(q => q.Id).EqualsTo(10);
                    facet.Excludes = new[] { "tag1", "tag2" };
                };
                var expected6 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""range"": {
                        ""field"": ""id"",
                        ""domain"": {
                          ""excludeTags"": [
                            ""tag1"",
                            ""tag2""
                          ],
                          ""filter"": ""id:10""
                        },
                        ""gap"": ""1"",
                        ""start"": ""10"",
                        ""end"": ""20""
                      }
                    }
                  }
                }");

                return new[]
                {
                    new object[] { config1, expected1 },
                    new object[] { config2, expected2 },
                    new object[] { config3, expected3 },
                    new object[] { config4, expected4 },
                    new object[] { config5, expected5 },
                    new object[] { config6, expected6 }
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data1))]
        public void FacetRangeParameterTheory001(Action<IFacetRangeParameter<TestDocument>> config, JObject expectd)
        {
            // Arrange
            var container = new JObject();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrConnection);
            expressionBuilder.LoadDocument();
            var serviceProvider = new Mock<ISolrExpressServiceProvider<TestDocument>>();
            serviceProvider
                .Setup(q => q.GetService<IFacetRangeParameter<TestDocument>>())
                .Returns(new FacetRangeParameter<TestDocument>(expressionBuilder, serviceProvider.Object));
            serviceProvider
                .Setup(q => q.GetService<SearchQuery<TestDocument>>())
                .Returns(new SearchQuery<TestDocument>(null));
            var parameter = (IFacetRangeParameter<TestDocument>)new FacetRangeParameter<TestDocument>(expressionBuilder, serviceProvider.Object);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expectd.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a FacetRangeParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void FacetRangeParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetRangeParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
