using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Validation;
using SolrExpress.Search.Query;
using SolrExpress.Solr5.Search.Parameter;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class FacetQueryParameterTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                var expressionBuilder = new ExpressionBuilder<TestDocument>(new SolrExpressOptions());
                expressionBuilder.LoadDocument();

                Action<IFacetQueryParameter<TestDocument>> config1 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                };
                var expected1 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""query"": {
                        ""q"": ""avg('Y')""
                      }
                    }
                  }
                }");

                Action<IFacetQueryParameter<TestDocument>> config2 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                };
                var expected2 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""query"": {
                        ""q"": ""avg('Y')"",
                        ""mincount"": 1
                      }
                    }
                  }
                }");

                Action<IFacetQueryParameter<TestDocument>> config3 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                };
                var expected3 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""query"": {
                        ""q"": ""avg('Y')"",
                        ""mincount"": 1,
                        ""sort"": {
                          ""count"": ""asc""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetQueryParameter<TestDocument>> config4 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                    facet.Limit = 10;
                };
                var expected4 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""query"": {
                        ""q"": ""avg('Y')"",
                        ""mincount"": 1,
                        ""limit"": 10,
                        ""sort"": {
                          ""count"": ""asc""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetQueryParameter<TestDocument>> config5 = facet =>
                {
                    facet.Query = new SearchQuery<TestDocument>(expressionBuilder).AddValue("avg('Y')", false);
                    facet.AliasName = "X";
                    facet.Minimum = 1;
                    facet.SortType = FacetSortType.CountAsc;
                    facet.Limit = 10;
                    facet.Excludes = new string[] { "tag1", "tag2" };
                };
                var expected5 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""X"": {
                      ""query"": {
                        ""q"": ""avg('Y')"",
                        ""domain"": {
                          ""excludeTags"": [
                            ""tag1"",
                            ""tag2""
                          ]
                        },
                        ""mincount"": 1,
                        ""limit"": 10,
                        ""sort"": {
                          ""count"": ""asc""
                        }
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
                    new object[] { config5, expected5 }
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void FacetQueryParameterTheory001(Action<IFacetQueryParameter<TestDocument>> config, JObject expectd)
        {
            // Arrange
            var container = new JObject();
            var parameter = (IFacetQueryParameter<TestDocument>)new FacetQueryParameter<TestDocument>();
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            var actual = string.Join("&", container);

            Assert.Equal(expectd.ToString(), actual.ToString());
        }

        /// <summary>
        /// Where   Using a FacetQueryParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void FacetQueryParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetQueryParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
