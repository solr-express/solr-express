﻿using Moq;
using Newtonsoft.Json.Linq;
using SolrExpress.Builder;
using SolrExpress.Configuration;
using SolrExpress.Options;
using SolrExpress.Search;
using SolrExpress.Search.Parameter;
using SolrExpress.Search.Parameter.Extension;
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
    public class FacetFieldParameterTests
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                var solrOptions = new SolrExpressOptions();
                var solrConnection = new FakeSolrConnection<TestDocument>();
                var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
                var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
                expressionBuilder.LoadDocument();

                Action<IFacetFieldParameter<TestDocument>> config1 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                };
                var expected1 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id""
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config2 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1);
                };
                var expected2 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config3 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc);
                };
                var expected3 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1,
                        ""sort"": {
                          ""count"": ""asc""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config4 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10);
                };
                var expected4 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""mincount"": 1,
                        ""sort"": {
                          ""count"": ""asc""
                        },
                        ""limit"": 10
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config5 = facet =>
                {
                    facet.FieldExpression(q => q.Id).Minimum(1).SortType(FacetSortType.CountAsc).Limit(10).Excludes("tag1", "tag2");
                };
                var expected5 = JObject.Parse(@"
                {
                    ""facet"": {
                        ""id"": {
                            ""terms"": {
                            ""field"": ""id"",
                            ""mincount"": 1,
                            ""domain"": {
                                ""excludeTags"": [
                                ""tag1"",
                                ""tag2""
                                ]
                            },
                            ""sort"": {
                                ""count"": ""asc""
                            },
                            ""limit"": 10
                            }
                        }
                    }
                }");

                Action<IFacetFieldParameter<TestDocument>> config6 = facet =>
                {
                    facet
                        .FieldExpression(q => q.Id)
                        .FacetField(q => q.Id);
                };
                var expected6 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                          ""facet"": {
                            ""id"": {
                              ""terms"": {
                                ""field"": ""id""
                              }
                            }
                          }
                        }
                      }
                    }
                  }");

                Action<IFacetFieldParameter<TestDocument>> config7 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.Filter(filter => filter.Field(q => q.Id).EqualsTo(10));
                };
                var expected7 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""domain"": {
                        ""filter"":""id:10""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config8 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.MethodType(FacetMethodType.DocValues);
                };
                var expected8 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""domain"": {
                        ""method"":""method:dv""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config9 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.MethodType(FacetMethodType.Stream);
                };
                var expected9 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""domain"": {
                        ""method"":""method:stream""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config10 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.MethodType(FacetMethodType.UninvertedField);
                };
                var expected10 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""domain"": {
                        ""method"":""method:uif""
                        }
                      }
                    }
                  }
                }");

                Action<IFacetFieldParameter<TestDocument>> config11 = facet =>
                {
                    facet.FieldExpression(q => q.Id);
                    facet.Prefix("xpto");
                };
                var expected11 = JObject.Parse(@"
                {
                  ""facet"": {
                    ""id"": {
                      ""terms"": {
                        ""field"": ""id"",
                        ""domain"": {
                        ""prefix"":""xpto""
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
                    new object[] { config5, expected5 },
                    new object[] { config6, expected6 },
                    new object[] { config7, expected7 },
                    new object[] { config8, expected8 },
                    new object[] { config9, expected9 },
                    new object[] { config10, expected10 },
                    new object[] { config11, expected11 }
                };
            }
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Invoking method "Execute" using happy path configurations
        /// What    Create correct SOLR instructions
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void FacetFieldParameterTheory001(Action<IFacetFieldParameter<TestDocument>> config, JObject expectd)
        {
            // Arrange
            var container = new JObject();
            var solrOptions = new SolrExpressOptions();
            var solrConnection = new FakeSolrConnection<TestDocument>();
            var solrDocumentConfiguration = new SolrDocumentConfiguration<TestDocument>();
            var expressionBuilder = new ExpressionBuilder<TestDocument>(solrOptions, solrDocumentConfiguration, solrConnection);
            expressionBuilder.LoadDocument();
            var serviceProvider = new Mock<ISolrExpressServiceProvider<TestDocument>>();
            serviceProvider
                .Setup(q => q.GetService<IFacetFieldParameter<TestDocument>>())
                .Returns(new FacetFieldParameter<TestDocument>(expressionBuilder, serviceProvider.Object));
            serviceProvider
                .Setup(q => q.GetService<SearchQuery<TestDocument>>())
                .Returns(new SearchQuery<TestDocument>(expressionBuilder));
            var parameter = (IFacetFieldParameter<TestDocument>)new FacetFieldParameter<TestDocument>(expressionBuilder, serviceProvider.Object);
            config.Invoke(parameter);

            // Act
            ((ISearchItemExecution<JObject>)parameter).Execute();
            ((ISearchItemExecution<JObject>)parameter).AddResultInContainer(container);

            // Assert
            Assert.Equal(expectd.ToString(), container.ToString());
        }

        /// <summary>
        /// Where   Using a FacetFieldParameter instance
        /// When    Checking custom attributes of class
        /// What    Has FieldMustBeIndexedTrueAttribute
        /// </summary>
        [Fact]
        public void FacetFieldParameter001()
        {
            // Arrange / Act
            var fieldMustBeIndexedTrueAttribute = typeof(FacetFieldParameter<TestDocument>)
                .GetTypeInfo()
                .GetCustomAttribute<FieldMustBeIndexedTrueAttribute>(true);

            // Assert
            Assert.NotNull(fieldMustBeIndexedTrueAttribute);
        }
    }
}
